using Microsoft.AspNetCore.Mvc; 
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

        public ShopKeeperController(IUserService authenticateService,IShopKeeperService shopKeeperService)
        {
            this.UserService = authenticateService;
            this.shopKeeperService = shopKeeperService;
        }
        [HttpGet]
        public Task<List<ShopDetails>> GeShopDetails()
        {
            return  shopKeeperService.GeShopDetails();
        }
    }
}
