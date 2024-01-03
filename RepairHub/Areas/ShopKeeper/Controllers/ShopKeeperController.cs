using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using Services.Shopkeeper;
 
using static Model.ShopDetails.ShopModels;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace RepairHub.Areas.ShopKeeper.Controllers
{
    [Authorize(Roles="Admin")]
    [Route("api/Shopkeeper/[controller]/[Action]")]
    [ApiController]
    public class ShopKeeperController : ControllerBase
    {
 
        private readonly IShopKeeperService shopKeeperService;

        public ShopKeeperController(IShopKeeperService shopKeeperService)
        {
 
            this.shopKeeperService = shopKeeperService;
        }
 
        [HttpGet]
        public Task<List<ShopDetails>> GetShopDetails()
        {
            return  shopKeeperService.GeShopDetails();
        }
    }
}
