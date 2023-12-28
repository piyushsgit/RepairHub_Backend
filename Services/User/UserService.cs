using Common.CommonMethods;
using Common.Helper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Model.AppSettingsJason;
using Model.UsersModels;
using Repository.User;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Ocsp;
using Model.dbModels;
using Model.ShopDetails;

namespace Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _accountRepository;
        private readonly INonStaticCommonMethods _nonStatic;
        public IConfiguration _connectionString;

        #region Constructors
        public UserService(IConfiguration connection, IUserRepository accountRepository, INonStaticCommonMethods nonStatic)
        {
            _connectionString = connection;
            _accountRepository = accountRepository;
            _nonStatic = nonStatic;
        }
        #endregion

        public async Task<ApiPostResponse<LoginModelResponse>> Loginuser(LoginWithContact model)
        {
            var res = new ApiPostResponse<LoginModelResponse>();
            LoginModelResponse loginModelResponse = new LoginModelResponse();
            loginModelResponse = await _accountRepository.UserLogin(model);
            if (loginModelResponse.message == "email not exists" || loginModelResponse.message == "ContactNo not exists" || loginModelResponse.message == "Otp Expired" || loginModelResponse.message == "please enter your otp")
            {
                res.Success = false;
                res.Message = loginModelResponse.message;
                return res;
            }
            else
            {
                res.Data = new LoginModelResponse
                {
                    JwdToken = Login(model.ContactNo, "User")
                };
                res.Success = true;
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
        }
        public async Task<ApiPostResponse<LoginModelResponse>> AdminLogin(LoginWithEmail model)
        {
            var res = new ApiPostResponse<LoginModelResponse>();
            LoginModelResponse loginModelResponse = new LoginModelResponse();
            loginModelResponse = await _accountRepository.AdminLogin(model);
            if (loginModelResponse.message == "please enter valid credentials")
            {
                res.Success = false;
                res.Message = ErrorMessages.LoginError;
                return res;
            }
            else if (loginModelResponse.message == "email not exists")
            {
                res.Success = false;
                res.Message = loginModelResponse.message;
                return res;
            }
            {
                res.Data = new LoginModelResponse
                {
                    JwdToken = Login(model.Email, "Admin")
                };
                res.Success = true;
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
        }
        public async Task<OtpVerificationResponse> Generateopt(string? ContactNo, Email? req)
        {
            if (string.IsNullOrEmpty(req.EmailId))
            {
                return await _accountRepository.Generateopt(ContactNo, null);
            }
            var result = await _accountRepository.Generateopt(null, req.EmailId);
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

        public string Login(string Data, string Role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_nonStatic.GetConfigurationValue(AppSettingsJason.AppSettings.ConnectionString));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Data),
                    new Claim(ClaimTypes.Role, Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_nonStatic.GetConfigurationValue(AppSettingsJason.JwtToken.TimeOutMin))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public async Task<Message> ForgotPassword(ForgotPassword forgot)
        {
            var Message = await _accountRepository.ForgotPassword(forgot);
            return await _accountRepository.ForgotPassword(forgot);
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

            // Check if an image is uploaded
            if (regData.image != null)
            {
                string uniqueFileName = Guid.NewGuid() + "_" + regData.image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await regData.image.CopyToAsync(stream);
                }
                regData.ProfileImage = uniqueFileName;
            }
            else
            {
                regData.ProfileImage = null;
            }
            RegistrationModel model = new RegistrationModel()
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


        #region
        public async Task<ApiPostResponse<LoginModelResponse>> SignInGoogle(SignInGoogle userLogin)
        {
            var res = new ApiPostResponse<LoginModelResponse>();
            var data = await _accountRepository.SignInGoogle(userLogin);
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
                    JwdToken = Login(userLogin.Email, "User"),
                    Id= data.Id,
                    userTypeId=data.userTypeId,
                    tokenExpiration= _nonStatic.GetConfigurationValue(AppSettingsJason.JwtToken.TimeOutMin),
                    EmailId = data.EmailId,
                    profileImage=data.profileImage,
                    IsVarified=data.IsVarified,
                    First_Name=data.First_Name

                };
                res.Success = true;
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
        }
        #endregion
        public async Task<List<TopBrands>> GetShopBrandsAsync()
        {
            return await _accountRepository.GetShopBrandsAsync();
        }
        public async Task<List<SearchData>> GetSearchDataAsync(string SearchParameter)
        {
            return await _accountRepository.GetSearchDataAsync(SearchParameter);
        }
    }
}
