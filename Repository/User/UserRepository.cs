using Common.Helper;
using Dapper;
using Data; 
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model; 
using Model.UsersModels; 
using System.Data;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Model.UsersModels.LoginModel;

namespace Repository.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public readonly IOptions<AppSettings> _connectionString;
        public UserRepository(IOptions<AppSettings> connection) : base(connection)
        {
            _connectionString = connection; 
        }
         
        public async Task<LoginModelResponse> UserLogin(LoginModel userLogin)
        {
           
                var param = new DynamicParameters();
                param.Add("@EmailId", userLogin.Email);
                param.Add("@Password", userLogin.Password);
                param.Add("@contactNo", userLogin.ContactNo);
                param.Add("@otp", userLogin.Otp);
                return await QueryFirstOrDefaultAsync<LoginModelResponse>(StoreProcedures.UserLogin, param, commandType: CommandType.StoredProcedure); 
          
        }

  
    }
}

