﻿using Common.CommonMethods;
using Common.Helper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using MimeKit;
using Model.AppSettingsJason;
using Model.dbModels;
using Model.RequestModel;
using Model.ShopDetails;
using Model.UsersModels;
using Repository.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Org.BouncyCastle.Ocsp;
using Model.dbModels;
using Model.ShopDetails;
using Model.RequestModel;
using Microsoft.AspNetCore.Http;


namespace Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _accountRepository;
        private readonly INonStaticCommonMethods _nonStatic;
        private readonly IHttpContextAccessor httpContextAccessor;
        public IConfiguration _connectionString;

        #region Constructors
        public UserService(IConfiguration connection, IUserRepository accountRepository, INonStaticCommonMethods nonStatic, IHttpContextAccessor HttpContextAccessor)
        {
            _connectionString = connection;
            _accountRepository = accountRepository;
            _nonStatic = nonStatic;
            httpContextAccessor = HttpContextAccessor;
        }
        #endregion

        public async Task<ApiPostResponse<LoginModelResponse>> LoginWithContact(LoginWithContact model)
        {
            var res = new ApiPostResponse<LoginModelResponse>();
            var roll ="";
            LoginModelResponse loginModelResponse = new LoginModelResponse();
            loginModelResponse = await _accountRepository.UserLogin(model);
            if (loginModelResponse.UserTypeId == 1)
                roll = Rolls.Admin;
            else if (loginModelResponse.UserTypeId == 2)
                roll = Rolls.Shopkeeper;
            else
                roll = Rolls.User; 
            if (loginModelResponse.message == "email not exists" || loginModelResponse.message == "ContactNo not exists" || loginModelResponse.message == "Otp Expired" || loginModelResponse.message == "please enter your otp")
            {
                res.Success = false;
                res.Message = loginModelResponse.message;
                return res;
            }
            else
            {
                 var encryptUserId = StaticMethods.GetEncrypt(loginModelResponse.Id.ToString());
                res.Data = new LoginModelResponse
                {
                    EncryptedId = encryptUserId,
                    EmailId = loginModelResponse.EmailId,
                    JwdToken = GenerateJwtToken(model.ContactNo, roll),
                    UserTypeId = loginModelResponse.UserTypeId,
                    IsVarified = loginModelResponse.IsVarified,
                    tokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_nonStatic.GetConfigurationValue(AppSettingsJason.JwtToken.TimeOutMin))),
                    First_Name = loginModelResponse.First_Name,
                    profileImage = loginModelResponse.profileImage
                };
                res.Success = true;
                
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
        }
        public async Task<ApiPostResponse<LoginModelResponse>> AdminLogin(LoginWithEmail model)
        {
            var res = new ApiPostResponse<LoginModelResponse>();
            var roll = "";
            LoginModelResponse loginModelResponse = new LoginModelResponse();
            loginModelResponse = await _accountRepository.AdminLogin(model);
            if (loginModelResponse.UserTypeId == 1)
                roll = Rolls.Admin;
            else if (loginModelResponse.UserTypeId == 2)
                roll = Rolls.Shopkeeper;
            else
                roll = Rolls.User;
            if (loginModelResponse.message == "please enter valid credentials"|| loginModelResponse.message == "Otp Expired"|| loginModelResponse.message == "email not exists")
            {
                res.Success = false;
                res.Message = loginModelResponse.message;
                return res;
            }
            else
            {
                var encryptUserId = StaticMethods.GetEncrypt(loginModelResponse.Id.ToString());
                res.Data = new LoginModelResponse
                {
                    EncryptedId = encryptUserId,
                    EmailId = loginModelResponse.EmailId,
                    JwdToken = GenerateJwtToken(model.Email, roll),
                    UserTypeId = loginModelResponse.UserTypeId,
                    IsVarified = loginModelResponse.IsVarified,
                    tokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_nonStatic.GetConfigurationValue(AppSettingsJason.JwtToken.TimeOutMin))),
                    First_Name =loginModelResponse.First_Name,
                    profileImage=loginModelResponse.profileImage
                };
                res.Success = true;
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
        }
        public async Task<OtpVerificationResponse> Generateopt(string? ContactNo, Email? req)
        {
            if (!string.IsNullOrEmpty(ContactNo))
            { 
            return await _accountRepository.Generateopt(ContactNo,null);
            } 
            var result = await _accountRepository.Generateopt(null,req.EmailId); 
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Repaihub", _nonStatic.GetConfigurationValue(AppSettingsJason.EmailSettings.UserName)));
            message.To.Add(new MailboxAddress(req.EmailId, req.EmailId));
            if (req.type == 1) // for email verication
            {
                message.Subject = "Email Verification";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"Please verify your Email Your Otp is: {result.otp} .( Your OTP is only valid for 2 Minutes)";
                message.Body = bodyBuilder.ToMessageBody();
            }
            else if (req.type == 2) // for Forgot Password
            {
                message.Subject = "Forgot Password";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $" Your Otp is: {result.otp} for Forgot Password.Please don't share If your are not trying to change your pasword. (Your OTP is only valid for 2 Minutes) ";
                message.Body = bodyBuilder.ToMessageBody();
            }
            else if (req.type == 3) // for Change Password
            {
                message.Subject = "Change Password";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $" Your Otp is: {result.otp} for Change Password.Please don't share If your are not trying to change your pasword. (Your OTP is only valid for 2 Minutes) ";
                message.Body = bodyBuilder.ToMessageBody();
            }

            using var client = new SmtpClient();
            await client.ConnectAsync(_nonStatic.GetConfigurationValue(AppSettingsJason.EmailSettings.SmtpServer), Convert.ToInt32(_nonStatic.GetConfigurationValue(AppSettingsJason.EmailSettings.Port)), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_nonStatic.GetConfigurationValue(AppSettingsJason.EmailSettings.UserName), _nonStatic.GetConfigurationValue(AppSettingsJason.EmailSettings.Password));
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return result;
        }
        public string GenerateJwtToken(string data, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Srbhgjbh@123saurabhGajbhiye123456");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, data),
            new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_nonStatic.GetConfigurationValue(AppSettingsJason.JwtToken.TimeOutMin))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        public async Task<Message> VerifyEmail(ForgotPasswordAndVerifyEmail verify)
        {
            verify.Type = 1;
            return await _accountRepository.ForgotPasswordAndVerifyEmail(verify);
        }
        public async Task<Message> ForgotPassword(ForgotPasswordAndVerifyEmail forgot)
        {
            forgot.Type = 2;
            return await _accountRepository.ForgotPasswordAndVerifyEmail(forgot); 
        }
        public TokenModel GetUserTokenData(string jwtToken = null)
        {
            string Token = string.Empty;
            if (string.IsNullOrEmpty(jwtToken))
                Token = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace(JwtBearerDefaults.AuthenticationScheme, "");
            else
                Token = jwtToken; 

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(Token.Trim());
            IEnumerable<Claim> claims = securityToken.Claims;

            TokenModel tokenModel = new TokenModel();
            if (claims != null && claims.ToList().Count > 0)
            {
                tokenModel.UserName = claims.ToList().FirstOrDefault().Value; 
                tokenModel.ValidTo = securityToken.ValidTo;
            }
            return tokenModel;
        }
        #region Register user
        //public static string GetHash(string input)
        //{
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {

        //        byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));


        //        StringBuilder builder = new StringBuilder();


        //        for (int i = 0; i < data.Length; i++)
        //        {
        //            builder.Append(data[i].ToString("x2"));
        //        }

        //        return builder.ToString();
        //    }
        //}
        #endregion
        public async Task<ApiPostResponse<int>> RegisterUser(RegistrationUserModel regData)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();

            if (regData == null || regData.image == null || regData.image.Length == 0)
            {
                response.Success = false;
                return response;
            }

            // Define the directory path where you want to save the images
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfileImages");
            regData.ProfileImage = await StaticMethods.SaveImageAsync(regData.image, uploadsFolder);

            RegistrationModel model = new()
            {
                FirstName = regData.FirstName,
                LastName = regData.LastName,
                Password = regData.Password,
                ContactNo = regData.ContactNo,
                EmailId = regData.EmailId, 
                ProfileImage = regData.ProfileImage,
                UserTypeId = 3
            };

            var result = await _accountRepository.RegisterUser(model);

            if (result == 1)
            {
                response.Data = result;
                response.Success = true;
                response.Message = ErrorMessages.CustomerRegistrationSuccess;
            }
            else
            {
                response.Success = false;
                response.Message = "Failure";
            }
            return response;

        }
        public async Task<List<ShopDetails>> GetFilterShopAsync(string FilterType, int Rating, int PageSize, int PageNumber)
        {
            return await _accountRepository.GetFilterShopAsync(FilterType, Rating, PageSize, PageNumber);
        }
        public async Task<List<ShopTypes>> GetShopTypeAsync()
        {
            return await _accountRepository.GetShopTypeAsync();
        }
        #region Siginwithgoogle
        public async Task<ApiPostResponse<LoginModelResponse>> SignInGoogle(SignInGoogle userLogin)
        {
            var res = new ApiPostResponse<LoginModelResponse>();
            var roll ="";
            var data = await _accountRepository.SignInGoogle(userLogin);
            if (data.UserTypeId == 2)
                roll = Rolls.Shopkeeper;
            else if (data.UserTypeId == 3)
                roll = Rolls.User;
            if (data == null)
            {
                res.Success = false;
                res.Message = data.message;
                return res;
            }
            else
            {
                res.Data = new LoginModelResponse
                {
                    JwdToken = GenerateJwtToken(userLogin.Email, roll),
                    Id = data.Id,
                    UserTypeId = data.UserTypeId,
                    tokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_nonStatic.GetConfigurationValue(AppSettingsJason.JwtToken.TimeOutMin))),
                    EmailId = data.EmailId,
                    profileImage = data.profileImage,
                    IsVarified = data.IsVarified,
                    First_Name = data.First_Name

                };
                res.Success = true;
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
        }
        #endregion
        #region Request Insert
        public async Task<ApiPostResponse<string>> InsertRequest(InsertRequestmodel req)
        {
            ApiPostResponse<string> response = new ApiPostResponse<string>();

            //if (req == null || req.RequestImage == null || req.RequestImage.Length == 0)
            //{
            //    response.Success = false;
            //    return response;
            //}


            //List<string> encryptedRequestFilePaths = new List<string>();
            //// Define the directory path where you want to save the images
            //string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "RequestImages");

            //foreach (var requestImage in req.RequestImage)
            //{
            //    string encryptedRequestFilePath = await StaticMethods.SaveImageAsync(requestImage, uploadsFolder);

            //    if (encryptedRequestFilePath != null)
            //    {
            //        encryptedRequestFilePaths.Add(encryptedRequestFilePath);
            //    }
            //}
            //req.RequestImageName = encryptedRequestFilePaths;
            // Check if an image is uploaded

            var result = await _accountRepository.InsertRequest(req);

            if (result != null)
            {
                string id = StaticMethods.GetEncrypt(result.RequestId.ToString());
                response.Data = id;
                response.Success = true;
                response.Message = ErrorMessages.RequestGenrated;
            }
            else
            {
                response.Success = false;
                response.Message = ErrorMessages.FailToGenerateRequest;
            }
            return response;
        }
        #endregion
        #region RequestStatus

        public async Task<ApiPostResponse<List<statusModel>>> RequestStauts(string requestId)
        {
            ApiPostResponse<List<statusModel>> response = new ApiPostResponse<List<statusModel>>();

            var request = StaticMethods.GetDecrypt(requestId);
            if (int.TryParse(request, out int parsedRequest))
            {

                var data = await _accountRepository.RequestStauts(parsedRequest);
                if (data != null)
                {
                    response.Data = data;
                    response.Success = true;
                    response.Message = ErrorMessages.Success;

                }

                return response;
            }
            else
            {
                // Handle the case where the decryption result couldn't be converted to an integer

                response.Success = false;
                response.Message = ErrorMessages.Error;
                return response;
            }


        }
        #endregion

        #region GetUserAddress
        public async Task<ApiPostResponse<List<GetAddress>>> GetUserAddreess(string userId)
        {
            ApiPostResponse<List<GetAddress>> response = new ApiPostResponse<List<GetAddress>>();
            int parsedUserId = Convert.ToInt32(userId);
            var data = await _accountRepository.GetUserAddreess(parsedUserId);

            if (data != null)
            {
                response.Data = data; response.Success = true; response.Message = ErrorMessages.Success;
            }
            else
            {
                response.Success = false; response.Message = ErrorMessages.Error;
            }
            return response;
        }
        #endregion
        public async Task<List<TopBrands>> GetShopBrandsAsync()
        {
            return await _accountRepository.GetShopBrandsAsync();
        }
        public async Task<List<SearchData>> GetSearchDataAsync(string SearchParameter, int PageSize, int PageNumber)
        {
            var data = await _accountRepository.GetSearchDataAsync(SearchParameter, PageSize, PageNumber);
            for (int i = 0; i < data.Count; i++)
            {
                data[i].EncryptshopId = StaticMethods.GetEncrypt(data[i].Id.ToString());
                data[i].Id = 0;
            }
            return data;
        }

        #region AddUpdateAddress
        public async Task<ApiPostResponse<int>> InsertAddress(AddressInsertModel address)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();

            var data = await _accountRepository.InsertAddress(address);

            if (data == 1)
            {
                response.Data = data; response.Success = true; response.Message = ErrorMessages.Success;
            }
            else
            {
                response.Success = false; response.Message = ErrorMessages.Error;
            }
            return response;
        }
        #endregion


        #region  uploadImage
        public async Task<ApiPostResponse<List<string>>> UploadImages(IFormFile[] images)
        {
            ApiPostResponse<List<string>> response = new ApiPostResponse<List<string>>();

            if (images == null || images.Length == 0)
            {
                response.Success = false;
                return response;
            }


            List<string> encryptedRequestFilePaths = new List<string>();
            // Define the directory path where you want to save the images
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "RequestImages");

            foreach (var requestImage in images)
            {
                string encryptedRequestFilePath = await StaticMethods.SaveImageAsync(requestImage, uploadsFolder);

                if (encryptedRequestFilePath != null)
                {
                    encryptedRequestFilePaths.Add(encryptedRequestFilePath);
                }
            }
            
            if(encryptedRequestFilePaths.Count > 0)
            {
                response.Success = true;
                response.Data = encryptedRequestFilePaths;
                response.Message = "ImageUploadSuuccessfully";
            }
            else
            {
                response.Success = false;
            }
             return response;
        }

          

        #endregion
    }
}


