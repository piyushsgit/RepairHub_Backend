using Common.Helper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.UsersModels;
using Repository.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Model.UsersModels.LoginModel;

namespace Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _accountRepository;
        public readonly IOptions<AppSettings> _connectionString;

        #region Constructors
        public UserService(IOptions<AppSettings> connection, IUserRepository accountRepository)  
        {
            _connectionString = connection;
            _accountRepository = accountRepository;
        }
        #endregion

       
        public async Task<ApiResponse<LoginModelResponse>> Loginuser(LoginModel model)
        {
            var res = new ApiResponse<LoginModelResponse>();
            LoginModelResponse loginModelResponse = new  LoginModelResponse();
            loginModelResponse = await _accountRepository.UserLogin(model);
            if (loginModelResponse != null)
            {
                loginModelResponse.JwdToken = Login(model);
                res.Success = true;
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
            else
            {  
                
                res.Success = false;
                res.Message = ErrorMessages.LoginError;
                return res;
            } 
        }
        public string Login(LoginModel login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_connectionString.Value.JWT_Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "2")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
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
