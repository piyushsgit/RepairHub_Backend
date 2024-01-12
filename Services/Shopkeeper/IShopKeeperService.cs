using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.CaseRequestResponse;
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
        public Task<ApiPostResponse<List<RequestResponsemodel>>> GetShopRequests(ShopRequestQueryModel req);
        public Task<ApiPostResponse<RequestResponsemodel>> GetCaseInfo(string caseId);
        public Task<List<ShopDetailsById>> GetShopsDetailsBylocation(Location location);
    }
}
