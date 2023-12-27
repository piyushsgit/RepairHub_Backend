using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.ShopDetails;
using Model.UsersModels;
using Services.Shopkeeper;
using Services.User;
using static Model.ShopDetails.ShopModels;

namespace RepairHub.Areas.ShopKeeper.Controllers
{
    [Route("api/Shopkeeper/[controller]/[Action]")]
    [ApiController]
    public class ShopKeeperController : ControllerBase
    {
        private readonly IUserService UserService;
        private readonly IShopKeeperService shopKeeperService;

        public ShopKeeperController(IUserService authenticateService, IShopKeeperService shopKeeperService)
        {
            this.UserService = authenticateService;
            this.shopKeeperService = shopKeeperService;
        }
        [HttpGet]
        public Task<List<ShopDetails>> GetShopDetails()
        {
            return shopKeeperService.GeShopDetails();
        }

        [HttpGet]
        public Task<ShopDetailsById> GetShopDetailsById(long id)
        {
            return shopKeeperService.GetShopDetailsById(id);
        }
        [HttpPost]
        public  Task<ApiPostResponse<int>> RegisterShop ([FromForm] RegistrationModel regData)
        {
            return  shopKeeperService.RegisterShop(regData);
        }

        [HttpGet]
        public async Task<IActionResult> GetShopImage(int id)
        {
            return Ok(await shopKeeperService.GetShopImageById(id));
        }
    }
}
