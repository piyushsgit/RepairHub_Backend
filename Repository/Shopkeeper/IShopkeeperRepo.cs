using Model.ShopDetails;
using static Model.ShopDetails.ShopModels;

namespace Repository.Shopkeeper
{
    public interface IShopkeeperRepo
    {
        public Task<List<ShopDetails>> GetShopDetails();

        public Task<ShopDetailsById> GetShopDetailsById(long id);
        public Task<List<ImageModel>> GetShopImageById(long id);
    }
}
