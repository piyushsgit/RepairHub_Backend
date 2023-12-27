using Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.UsersModels;
using Services.User;


namespace RepairHub.Areas.Users.Controllers
{
    [Route("api/Users/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;

        public UserController(IUserService authenticateService)
        {
            this.UserService = authenticateService;
        }

        [HttpPost]
        public async Task<ApiPostResponse<LoginModelResponse>> LoginWithContact(LoginWithContact login)
        {
            return await UserService.Loginuser(login);
        }

        [HttpPost]
        public async Task<ApiPostResponse<LoginModelResponse>> LoginWithEmail(LoginWithEmail login)
        {
            return await UserService.AdminLogin(login);
        }

        [HttpPost]
        public async Task<OtpVerificationResponse> GenereateOtpForContact(string ContactNo)
        {
            return await UserService.Generateopt(ContactNo, null);
        }


        [HttpPost]
        public async Task<Message> ForgotPassword(ForgotPassword forgot)
        {
            return await UserService.ForgotPassword(forgot);
        }


        [HttpPost]
        public async Task<ApiPostResponse<int>> RegisterUser([FromForm] RegistrationUserModel regData)
        {
            return await UserService.RegisterUser(regData);
        }

        [HttpPost]
        public async Task<OtpVerificationResponse> SendOtpForEmailVerification(string EmailId)
        {
            var email = new Email();
            email.type = 1;
            email.EmailId = EmailId;
            return await UserService.Generateopt(null, email);
        }
        [HttpPost]
        public async Task<OtpVerificationResponse> SendOtpForForgotPassword(string EmailId)
        {
            var email = new Email();
            email.type = 2;
            email.EmailId = EmailId;
            return await UserService.Generateopt(null, email);
        }
        [HttpPost]
        public async Task<OtpVerificationResponse> SendOtpForChangePassword(string EmailId)
        {
            var email = new Email();
            email.type = 3;
            email.EmailId = EmailId;
            return await UserService.Generateopt(null, email);
        }


        [HttpGet]
        public async Task<IActionResult> FilterShop(string FilterType, int Rating, int PageSize, int PageNumber)
        {
            return  Ok(await UserService.GetFilterShopAsync(FilterType,Rating,PageSize, PageNumber));

        [HttpPost]
        public async Task<ApiPostResponse<LoginModelResponse>> SignInGoogle(SignInGoogle login)
        {
            
            return await UserService.SignInGoogle(login);

        }

        [HttpGet]
        public async Task<IActionResult> ShopTypes()
        {
            return Ok(await UserService.GetShopTypeAsync());
        }
        [HttpPost]
        public async Task<ApiPostResponse<int>> InsertRequest([FromForm]  InsertRequestmodel req)
        {
            return await UserService.InsertRequest(req);
        }
    }
}
