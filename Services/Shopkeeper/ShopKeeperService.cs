using Repository.Shopkeeper;
using static Model.ShopDetails.ShopModels;

namespace Services.Shopkeeper
{
    public class ShopKeeperService : IShopKeeperService
    {
        private readonly IShopkeeperRepo shopkeeperRepo;

        public ShopKeeperService(IShopkeeperRepo shopkeeperRepo)
        {
            this.shopkeeperRepo = shopkeeperRepo;
        }
        public Task<List<ShopDetails>> GeShopDetails()
        { 
            return shopkeeperRepo.GetShopDetails();
        }
         
    }
}
