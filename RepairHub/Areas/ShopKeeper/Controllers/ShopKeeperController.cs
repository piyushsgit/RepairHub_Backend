using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.ShopDetails;
using Model.UsersModels;
using Microsoft.AspNetCore.Authorization;

using Services.Shopkeeper;
 
using static Model.ShopDetails.ShopModels;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Services.User;

namespace RepairHub.Areas.ShopKeeper.Controllers
{
    [Authorize(Roles="Admin")]
    [Route("api/Shopkeeper/[controller]/[Action]")]
    [ApiController]
    public class ShopKeeperController : ControllerBase
    {
 
        private readonly IShopKeeperService shopKeeperService;

        public ShopKeeperController(IUserService authenticateService, IShopKeeperService shopKeeperService)
        {
 
            this.shopKeeperService = shopKeeperService;
        }
 
        [HttpGet]
        public Task<List<ShopDetails>> GetShopDetails()
        {
            return shopKeeperService.GeShopDetails();
        }

        [HttpGet]
        public Task<ShopDetailsById> GetShopDetailsById(string id)
        {
            return shopKeeperService.GetShopDetailsById(id);
        }
        [HttpPost]
        public  Task<ApiPostResponse<int>> RegisterShop ([FromForm] RegistrationModel regData)
        {
            return  shopKeeperService.RegisterShop(regData);
        }

        [HttpGet]
        public async Task<IActionResult> GetShopImage(string id)
        {
            return Ok(await shopKeeperService.GetShopImageById(id));
        }
    }
}
