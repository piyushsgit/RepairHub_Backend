﻿using Common.Helper;
using Model.dbModels;
using Model.UsersModels; 
namespace Services.User
{
    public interface IUserService
    {
        public Task<ApiPostResponse<LoginModelResponse>> Loginuser(LoginWithContact model);
        public Task<ApiPostResponse<LoginModelResponse>> AdminLogin(LoginWithEmail model);
        public Task<OtpVerificationResponse> Generateopt(string? ContactNo, Email? email);
        public Task<Message> ForgotPassword(ForgotPassword forgot);


        public Task<ApiPostResponse<int>> RegisterUser(RegistrationUserModel regData);

        public Task<List<ShopDetails>> GetFilterShopAsync(string FilterType, int Rating, int PageSize, int PageNumber);
        public Task<List<ShopTypes>> GetShopTypeAsync();

        


        public Task<ApiPostResponse<LoginModelResponse>> SignInGoogle(SignInGoogle userLogin);
        public Task<ApiPostResponse< int>> InsertRequest(InsertRequestmodel req);
    }
}
