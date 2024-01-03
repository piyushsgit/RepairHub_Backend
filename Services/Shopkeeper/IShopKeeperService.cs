using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.ShopDetails;
using Model.UsersModels;
using static Model.ShopDetails.ShopModels;

namespace Services.Shopkeeper
{
    public interface IShopKeeperService
    {
        public Task<List<ShopDetails>> GeShopDetails();

        public Task<ShopDetailsById> GetShopDetailsById(string id);

        public Task<ApiPostResponse<int>> RegisterShop(RegistrationModel regData);
        public Task<List<ImageModel>> GetShopImageById(string id);
    }
}
