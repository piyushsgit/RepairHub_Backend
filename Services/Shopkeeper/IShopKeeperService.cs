using static Model.ShopDetails.ShopModels;

namespace Services.Shopkeeper
{
    public interface IShopKeeperService
    {
        public Task<List<ShopDetails>> GeShopDetails();
    }
}
