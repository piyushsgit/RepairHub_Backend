using Data;
using Model.dbModels;
using Model.UsersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository.User
{
    public interface IUserRepository
    {
        public Task<LoginModelResponse> UserLogin(LoginWithContact userLogin);
        public Task<LoginModelResponse> AdminLogin(LoginWithEmail userLogin);
        public Task<OtpVerificationResponse> Generateopt(string? ContactNo, string? email);
        public Task<Message> ForgotPassword(ForgotPassword forgot);


        public Task<int> RegisterUser(RegistrationModel userReg);

        public Task<List<ShopDetails>> GetFilterShopAsync(string FilterType, int Rating, int PageSize, int PageNumber);
        public Task<List<ShopTypes>> GetShopTypeAsync();


        public Task<LoginModelResponse> SignInGoogle(SignInGoogle userLogin);
        public Task<int> InsertRequest(InsertRequestmodel req);
    }
}
