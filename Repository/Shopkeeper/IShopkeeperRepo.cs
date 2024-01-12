﻿using Common.Helper;
using Model.CaseRequestResponse;
using Model.ShopDetails;
using static Model.ShopDetails.ShopModels;

namespace Repository.Shopkeeper
{
    public interface IShopkeeperRepo
    {
        public Task<List<ShopDetails>> GetShopDetails();

        public Task<ShopDetailsById> GetShopDetailsById(long id);
        public Task<List<ImageModel>> GetShopImageById(long id);
        public Task<List<ShopDetailsById>> GetShopsDetailsBylocation(Location location);
        public Task<List<RequestResponsemodel>> GetShopRequests(ShopRequestQueryModel req);
        public Task<RequestResponsemodel> GetCaseInfo(long caseId);
    }
}
