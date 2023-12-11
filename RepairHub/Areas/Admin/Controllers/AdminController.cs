using Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.UsersModels;
using Services.User;

namespace RepairHub.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]/[Action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService UserService;

        public AdminController(IUserService authenticateService)
        {
            this.UserService = authenticateService;
        }
        [HttpPost] 
        public async Task<ApiPostResponse<LoginModelResponse>> Login(LoginWithEmail login)
        {
            return await UserService.AdminLogin(login);
        }
    }
}
