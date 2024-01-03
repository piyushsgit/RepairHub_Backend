using Azure.Core;
using Common.CommonMethods;
using Common.Helper;
using Dapper;
using Data;
using Model.dbModels;
using Model.ShopDetails;
using Model.RequestModel;
using Model.UsersModels;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;


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

      
        public async Task<Message> ForgotPasswordAndVerifyEmail(ForgotPasswordAndVerifyEmail forgot)
        {
            var param = new DynamicParameters();
            param.Add("@Type", forgot.Type);
            param.Add("@Email", forgot.Email);
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
            if (userReg.UserTypeId != 3)
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

        public async Task<List<ShopDetails>> GetFilterShopAsync(string FilterType,int Rating, int PageSize,int PageNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FilterType", FilterType);
            parameters.Add("@Rating", Rating);
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageNumber", PageNumber);
            var result = await QueryAsync<ShopDetails>(StoreProcedures.FilterShop, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();

        }
        public async Task<List<ShopTypes>> GetShopTypeAsync()
        {

            var result = await QueryAsync<ShopTypes>(StoreProcedures.ShopType, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

       
    
        public async Task<List<TopBrands>> GetShopBrandsAsync()
        {
            var result = await QueryAsync<TopBrands>(StoreProcedures.GetBrands, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<List<SearchData>> GetSearchDataAsync(string SearchParameter,int PageSize, int PageNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchParameter", SearchParameter);
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageNumber", PageNumber);
            var result = await QueryAsync<SearchData>(StoreProcedures.GetSearchData, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();

        }

        public async Task<InsertRequestResponsemodel> InsertRequest(InsertRequestmodel req)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", req.UserId);
            parameters.Add("@ShopId", req.ShopId);
            parameters.Add("@Description", req.Description);
            parameters.Add("@UserAddressId", req.UserAddressId);
            parameters.Add("@CreatedBy", req.CreatedBy);
            parameters.Add("@RequestImageList", string.Join(",", req.RequestImageName));



            return await QueryFirstOrDefaultAsync<InsertRequestResponsemodel>("InsertRequest", parameters, commandType: CommandType.StoredProcedure);


        }
        public async Task<List<statusModel>> RequestStauts(int requestId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@requestId", requestId);

            return (await QueryAsync<statusModel>("[GetTimelineUpdate]", parameters, commandType: CommandType.StoredProcedure)).ToList();

        }
        public async Task<List<GetAddress>> GetUserAddreess(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@userid", userId);

            return (await QueryAsync<GetAddress>("GetAddressByUserId", parameters, commandType: CommandType.StoredProcedure)).ToList();

        }

        public async Task<int> InsertAddress(AddressInsertModel address)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", address.Id);
            parameters.Add("@UserId", address.UserId);
            parameters.Add("@ShopId", address.ShopId);
            parameters.Add("@Country", address.Country);
            parameters.Add("@State", address.State);
            parameters.Add("@City", address.City);
            parameters.Add("@Address", address.Address);
            parameters.Add("@Area", address.Area);
            parameters.Add("@AddressType", address.AddressType);
            parameters.Add("@Latitude", address.Latitude);
            parameters.Add("@Decription", address.Decription);
            parameters.Add("@Longitude", address.Longitude);

            return await ExecuteAsync<int>("UpdateInsertAddress", parameters, commandType: CommandType.StoredProcedure);
        }



    }
}

