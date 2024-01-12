using Common.CommonMethods;
using Common.Helper;
using Microsoft.AspNetCore.Http;
using Model.CaseRequestResponse;
using Model.ShopDetails;
using Model.UsersModels;
using Org.BouncyCastle.Ocsp;
using Repository.Shopkeeper;
using Repository.User;
using System.Collections.Generic;
using static Model.ShopDetails.ShopModels;

namespace Services.Shopkeeper
{
    public class ShopKeeperService : IShopKeeperService
    {
        private readonly IShopkeeperRepo shopkeeperRepo;
        private readonly IUserRepository accountRepository;

        public ShopKeeperService(IShopkeeperRepo shopkeeperRepo, IUserRepository accountRepository)
        {
            this.shopkeeperRepo = shopkeeperRepo;
            this.accountRepository = accountRepository;
        }
        public Task<List<ShopDetails>> GeShopDetails()
        {
            return shopkeeperRepo.GetShopDetails();
        }
        public  Task<ShopDetailsById> GetShopDetailsById(string id)
        {
             var decryptId=Convert.ToInt32(StaticMethods.GetDecrypt(id));
            return shopkeeperRepo.GetShopDetailsById(decryptId);
             
        }
        public async Task<ApiPostResponse<int>> RegisterShop(RegistrationModel regData)
        {
            ApiPostResponse<int> response = new ();

            string profileUploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfileImages");
            regData.ProfileImage = await StaticMethods.SaveImageAsync(regData.image, profileUploadsFolder);

            // Check if the request or IData is null or if the ShopImage array is empty
            if (regData == null || regData.ShopImage == null || regData.ShopImage.Length == 0)
            {
                response.Success = false;
                return response;
            }
            List<string> encryptedShopFilePaths = new ();

            string shopUploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ShopImages");

            foreach (var shopImage in regData.ShopImage)
            {
                string encryptedShopFilePath = await StaticMethods.SaveImageAsync(shopImage, shopUploadsFolder);

                if (encryptedShopFilePath != null)
                {
                    encryptedShopFilePaths.Add(encryptedShopFilePath);
                }
            }
            regData.ShopImageName = encryptedShopFilePaths;
            regData.UserTypeId = 2;
            var result = await accountRepository.RegisterUser(regData);
            if (result > 1)
            {
                response.Data = result;
                response.Success = true;
                response.Message = ErrorMessages.ShopkeeperRegistrationSuccess;
            }


            else
            {
                response.Success = false;
                response.Message = "Failure";
            }

            return response;

        } 
        private async Task<string> SaveImageAsync(IFormFile image, string uploadsFolder)
        {
            if (image == null)
            {
                return null;
            }

            string uniqueFileName = Guid.NewGuid() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
        public async Task<List<ImageModel>> GetShopImageById(string id)
        {
            var decryptId = Convert.ToInt32(StaticMethods.GetDecrypt(id));
            return await shopkeeperRepo.GetShopImageById(decryptId);
        }
        public async Task<ApiPostResponse<List<RequestResponsemodel>>> GetShopRequests(ShopRequestQueryModel req)
        {
            ApiPostResponse<List<RequestResponsemodel>> response = new();
            
            if(req.UserId!=null)
            {
                req.DecryptUserId =Convert.ToInt32( StaticMethods.GetDecrypt(req.UserId));

                var results = await shopkeeperRepo.GetShopRequests(req);

                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        result.EncryptRequstId = StaticMethods.GetEncrypt(result.RequestId.ToString());
                        result.RequestId = 0;
                    }

                    response.Data = results;
                    response.Success = true;
                    response.Message = ErrorMessages.Success;
                }
                else
                {
                    response.Success=false;
                    response.Message = ErrorMessages.Error;
                }
            }
            else
            {
                response.Success=false;
                response.Message=ErrorMessages.Error;
            }
            return response;
        }

        public async Task<ApiPostResponse<RequestResponsemodel>> GetCaseInfo(string caseId)
        {
            ApiPostResponse<RequestResponsemodel> response = new();
            RequestResponsemodel data = await shopkeeperRepo.GetCaseInfo(Convert.ToInt32(StaticMethods.GetDecrypt(caseId)));
            if (data != null)
            {
                data.EncryptRequstId = caseId;
                data.UserId =StaticMethods.GetEncrypt(data.UserId); 
                data.RequestId = 0;
                response.Data = data;
                response.Success = true;
                response.Message= ErrorMessages.Success;
            }
            else
            {
                response.Success=false;
                response.Message = ErrorMessages.Error;
            }
            return response;
        }
        public async Task<List<ShopDetailsById>> GetShopsDetailsBylocation(Location location)
        {
            List<ShopDetailsById> data = await shopkeeperRepo.GetShopsDetailsBylocation(location);

            data.ForEach(item => item.EncryptedId = StaticMethods.GetEncrypt(item.Id.ToString()));
            foreach (var shopDetails in data)
            {
                if (!string.IsNullOrEmpty(shopDetails.ImageNames))
                {
                    shopDetails.Images = shopDetails.ImageNames
                        .Split(',',','+' ')
                        .Select(imageName => new Image { ImageName = imageName })
                        .ToArray();
                }
            }

            return data;
        }


    }
}
