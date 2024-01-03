using Common.CommonMethods;
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
using Model.UsersModels;
using Newtonsoft.Json;
using Repository.User;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;


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

        public async Task<ApiPostResponse<LoginModelResponse>> Loginuser(LoginWithContact model)
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
                res.Data = new LoginModelResponse
                {
                    Id = loginModelResponse.Id,
                    EmailId = loginModelResponse.EmailId,
                    JwdToken = GenerateJwtToken(model.ContactNo, roll),
                    UserTypeId = loginModelResponse.UserTypeId,
                    IsVarified = loginModelResponse.IsVarified
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
                res.Data = new LoginModelResponse
                {
                    Id=loginModelResponse.Id,
                    EmailId=loginModelResponse.EmailId,
                    JwdToken = GenerateJwtToken(model.Email, roll),
                    UserTypeId = loginModelResponse.UserTypeId,
                    IsVarified=loginModelResponse.IsVarified
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


        public Task<Message> ForgotPassword(ForgotPassword forgot)
        {
            throw new NotImplementedException();
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

        #region Encrypt Password
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
    }
}
