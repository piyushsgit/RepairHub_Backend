using Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.UsersModels;
using Services.User;
using static Model.UsersModels.LoginModel;

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

        public async Task<ApiResponse<LoginModelResponse>> Login(LoginModel login)
        { 
            return await UserService.Loginuser(login);
        }

    }
}
