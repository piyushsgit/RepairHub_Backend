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

        public async Task<int> RegisterUser(RegistrationModel userReg)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", userReg.FirstName);
            parameters.Add("@LastName", userReg.LastName);
            parameters.Add("@ContactNo", userReg.ContactNo);
            parameters.Add("@EmailId",userReg.EmailId);
            parameters.Add("@UserTypeId", userReg.UserTypeId);
            parameters.Add("@CreatedBy", userReg.CreatedBy);
            parameters.Add("@ModifiedBy", userReg.ModifiedBy);
            parameters.Add("@Password", userReg.Password);
            parameters.Add("@ProfileImage", userReg.ProfileImage);
            parameters.Add("@ShopName", userReg.ShopName);
            parameters.Add("@ShopOwnerName", userReg.ShopOwnerName);
            parameters.Add("@AddharNumber", userReg.AddharNumber);
            parameters.Add("@PanNumber", userReg.PanNumber);
            parameters.Add("@ShopDescription", userReg.ShopDescription);
            parameters.Add("@ShopRepairType", userReg.ShopRepairType);
            parameters.Add("@Since", userReg.Since);
            parameters.Add("@AsociateWith", userReg.AsociateWith);
            parameters.Add("@Country", userReg.Country);
            parameters.Add("@State", userReg.State);
            parameters.Add("@City", userReg.City);
            parameters.Add("@Address", userReg.Address);
            parameters.Add("@Rating", userReg.Rating);
            parameters.Add("@Area", userReg.Area);
            parameters.Add("@AddressType", userReg.AddressType);
            parameters.Add("@AccountNo", userReg.AccountNo);
            parameters.Add("@AccountHolderName", userReg.AccountHolderName);
            parameters.Add("@BankName", userReg.BankName);
            parameters.Add("@IFSC_Code", userReg.IFSC_Code);
            parameters.Add("@UPI_Detail", userReg.UPI_Detail);

            int data = await ExecuteAsync<int>("SP_RegisterUser", parameters, commandType: CommandType.StoredProcedure);

            return data;
        }
    }
}

