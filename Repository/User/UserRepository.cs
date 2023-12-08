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
 

namespace Repository.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public readonly IOptions<AppSettings> _connectionString;
        public UserRepository(IOptions<AppSettings> connection) : base(connection)
        {
            _connectionString = connection; 
        }
         
        public async Task<LoginModelResponse> UserLogin(LoginWithContact userLogin)
        { 
                var param = new DynamicParameters(); 
                 param.Add("@contactNo", userLogin.ContactNo);
                 param.Add("@otp", userLogin.Otp);
                return await QueryFirstOrDefaultAsync<LoginModelResponse>(StoreProcedures.UserLogin, param, commandType: CommandType.StoredProcedure);
       
        }
        public async Task<LoginModelResponse> AdminLogin(LoginWithEmail userLogin)
        {

            var param = new DynamicParameters();
            param.Add("@EmailId", userLogin.Email);
            param.Add("@Password", userLogin.Password); 
            return await QueryFirstOrDefaultAsync<LoginModelResponse>(StoreProcedures.UserLogin, param, commandType: CommandType.StoredProcedure);

        }

        public async Task<OtpVerificationResponse> Generateopt(string ContactNo)
        {
            var param = new DynamicParameters();
            param.Add("@ContactNo", ContactNo);
            return await QueryFirstOrDefaultAsync<OtpVerificationResponse>(StoreProcedures.GenerateOtp , param, commandType: CommandType.StoredProcedure);
        }
    }
}

