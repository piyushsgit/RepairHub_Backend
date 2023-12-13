using Common.CommonMethods;
using Common.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.AppSettingsJason;
using Model.UsersModels;
using Repository.Shopkeeper;
using Repository.User;
using System.Data;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;


namespace Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _accountRepository;
        private readonly INonStaticCommonMethods _nonStatic;
        public  IConfiguration _connectionString;

        #region Constructors
        public UserService(IConfiguration connection, IUserRepository accountRepository,INonStaticCommonMethods nonStatic)
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
            if (loginModelResponse.message == "email not exists" || loginModelResponse.message == "ContactNo not exists"|| loginModelResponse.message == "Otp Expired" || loginModelResponse.message == "Inccorect Otp")

            {
                res.Success = false;
                res.Message = loginModelResponse.message;
                return res;
            } 
            else 
            {
                res.Data = new LoginModelResponse
                {
                    JwdToken = Login(model.ContactNo, "Admin") 
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
        public Task<OtpVerificationResponse> Generateopt(string ContactNo)
        {
            return _accountRepository.Generateopt(ContactNo);
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

    }
}
