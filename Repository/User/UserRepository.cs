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

        public async Task<OtpVerificationResponse> Generateopt(string? ContactNo, string? email)
        {
            var param = new DynamicParameters();
            param.Add("@ContactNo", ContactNo);
            param.Add("@email", email);
            return await QueryFirstOrDefaultAsync<OtpVerificationResponse>(StoreProcedures.GenerateOtp, param, commandType: CommandType.StoredProcedure);
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

        public async Task<int> RegisterUser(RegistrationModel userReg)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", userReg.FirstName);
            parameters.Add("@LastName", userReg.LastName);
            parameters.Add("@ContactNo", userReg.ContactNo);
            parameters.Add("@EmailId", userReg.EmailId);
            parameters.Add("@UserTypeId", userReg.UserTypeId);
            parameters.Add("@CreatedBy", userReg.CreatedBy);
            parameters.Add("@Password", userReg.Password);
            parameters.Add("@ProfileImage", userReg.ProfileImage);
            if(userReg.UserTypeId != 3)
            {
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
            parameters.Add("@Area", userReg.Area);
            parameters.Add("@AddressType", userReg.AddressType);
            parameters.Add("@AccountNo", userReg.AccountNo);
            parameters.Add("@AccountHolderName", userReg.AccountHolderName);
            parameters.Add("@BankName", userReg.BankName);
            parameters.Add("@IFSC_Code", userReg.IFSC_Code);
            parameters.Add("@UPI_Detail", userReg.UPI_Detail);
            parameters.Add("@shopImageList", string.Join(",", userReg.ShopImageName));
            }

            int data = await ExecuteAsync<int>("SP_RegisterUser", parameters, commandType: CommandType.StoredProcedure);

            return data;
        }

        public async Task<LoginModelResponse> SignInGoogle(SignInGoogle userLogin)
        {
            var param = new DynamicParameters();
            param.Add("@Email", userLogin.Email);
            param.Add("@FirstName", userLogin.FirstName);
            param.Add("@LastName", userLogin.LastName);
            param.Add("@ProfileImage", userLogin.ProfileImage);
            return await QueryFirstOrDefaultAsync<LoginModelResponse>("SignInWithGoogle", param, commandType: CommandType.StoredProcedure);
             
        }
    }
}

