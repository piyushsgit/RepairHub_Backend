using Common.CommonMethods;
using Common.Helper;
using Dapper;
using Data;
using Model.UsersModels;
using System.Data;


namespace Repository.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly INonStaticCommonMethods nonStatic;

        public UserRepository(INonStaticCommonMethods config) : base(config) 
        { 
            this.nonStatic = nonStatic;
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

        public async Task<OtpVerificationResponse> Generateopt(string? ContactNo,string? email)
        {
            var param = new DynamicParameters();
            param.Add("@ContactNo", ContactNo);
            param.Add("@email", email);
            return await QueryFirstOrDefaultAsync<OtpVerificationResponse>(StoreProcedures.GenerateOtp , param, commandType: CommandType.StoredProcedure);
        }
 

        public async Task<Message> ForgotPassword(ForgotPassword forgot)
        {
            var param = new DynamicParameters();
            param.Add("@Type", forgot.Type);
            param.Add("@Email", forgot.Email);
            param.Add("@MobileNo", forgot.contact);
            param.Add("@OTP", forgot.Otp);
            param.Add("@NewPassword", forgot.NewPassword);
 
            return await QueryFirstOrDefaultAsync<Message>(StoreProcedures.ForgotPassword, param, commandType: CommandType.StoredProcedure);
        }
    }
}

