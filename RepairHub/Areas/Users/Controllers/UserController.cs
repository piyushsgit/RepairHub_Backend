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
        public async Task<ApiPostResponse<LoginModelResponse>> Login(LoginWithContact login)
        {
            return await UserService.Loginuser(login);
        }

        [HttpPost]
        public async Task<OtpVerificationResponse> GenereateOtp(string ContactNo)
        {
            return await UserService.Generateopt(ContactNo);
        }

        [HttpPost]
        public async Task<ApiPostResponse<int>> RegisterUser([FromForm] RegistrationModel regData)
        {
            return await UserService.RegisterUser(regData);
        }
    }
}
