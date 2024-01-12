using Common.Helper;
using Model.dbModels;
using Model.ShopDetails;
using Model.RequestModel;
using Model.UsersModels;
using Microsoft.AspNetCore.Http;

namespace Services.User
{
    public interface IUserService
    {
        public Task<ApiPostResponse<LoginModelResponse>> LoginWithContact(LoginWithContact model);
        public Task<ApiPostResponse<LoginModelResponse>> AdminLogin(LoginWithEmail model);
        public Task<OtpVerificationResponse> Generateopt(string? ContactNo, Email? req);
        public Task<Message> VerifyEmail(ForgotPasswordAndVerifyEmail forgot);
        public Task<Message> ForgotPassword(ForgotPasswordAndVerifyEmail forgot);
        public Task<Message> ValidateOtp(ForgotPasswordAndVerifyEmail forgot);
        public TokenModel GetUserTokenData(string jwtToken = null);

        public Task<ApiPostResponse<int>> RegisterUser(RegistrationUserModel regData);


        public Task<List<ShopDetails>> GetFilterShopAsync(string FilterType, int Rating, int PageSize, int PageNumber);
        public Task<List<ShopTypes>> GetShopTypeAsync();

        public Task<List<TopBrands>> GetShopBrandsAsync();
        public Task<List<SearchData>> GetSearchDataAsync(string SearchParameter, int PageSize, int PageNumber);
       

        public Task<ApiPostResponse<LoginModelResponse>> SignInGoogle(SignInGoogle userLogin);
        

        public Task<ApiPostResponse<List<statusModel>>> RequestStauts(string requestId);
        public  Task<ApiPostResponse< List<GetAddress>>> GetUserAddreess(string userId);
        public Task<ApiPostResponse<int>> InsertAddress(AddressInsertModel address);

        public Task<ApiPostResponse<List<string>>> UploadImages(IFormFile[] images);
    }
}
