using Common.CommonMethods;
using Common.Helper;
using Microsoft.AspNetCore.Http;
using Model.UsersModels;
using Org.BouncyCastle.Ocsp;
using Repository.Shopkeeper;
using Repository.User;
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
        public  Task<ShopDetailsById> GetShopDetailsById(long id)
        {
            return shopkeeperRepo.GetShopDetailsById(id);
             
        }
        public async Task<ApiPostResponse<int>> RegisterShop(RegistrationModel regData)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();

            string profileUploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfileImages");
            regData.ProfileImage = await StaticMethods.SaveImageAsync(regData.image, profileUploadsFolder);

            // Check if the request or IData is null or if the ShopImage array is empty
            if (regData == null || regData.ShopImage == null || regData.ShopImage.Length == 0)
            {
                response.Success = false;
                return response;
            }
            List<string> encryptedShopFilePaths = new List<string>();

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


        
    }
}
