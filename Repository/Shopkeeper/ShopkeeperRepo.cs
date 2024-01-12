using Common.CommonMethods;
using Common.Helper;
using Dapper;
using Data;
using Model.CaseRequestResponse;
using Model.ShopDetails;
using System.Data;
using System.Data.Common;
using System.Runtime.Intrinsics.Arm;
using static Model.ShopDetails.ShopModels;

namespace Repository.Shopkeeper
{
    public class ShopkeeperRepo : BaseRepository, IShopkeeperRepo
    {
        private readonly INonStaticCommonMethods nonStatic;

        public ShopkeeperRepo(INonStaticCommonMethods config) : base(config)
        {
            this.nonStatic = nonStatic;
        }
        public async Task<List<ShopDetails>> GetShopDetails()
        {
            return (await QueryAsync<ShopDetails>(StoreProcedures.GetShopDetails, null, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<ShopDetailsById> GetShopDetailsById(long id)
        {
            var dp = new DynamicParameters();
            dp.Add("@Id", id);
            return await QueryFirstOrDefaultAsync<ShopDetailsById>(StoreProcedures.GetShopDetailsById, dp, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<ImageModel>> GetShopImageById(long id)
        {
            var dp = new DynamicParameters();
            dp.Add("@ShopId", id);
            var result = await QueryAsync<ImageModel>(StoreProcedures.GetImages, dp, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<RequestResponsemodel>> GetShopRequests(ShopRequestQueryModel req)
        {
            DynamicParameters parameters = new();
            parameters.Add("@UserId", req.DecryptUserId);
            parameters.Add("@Search", req.Search);
            parameters.Add("@OrderBy", req.OrderBy);
            parameters.Add("@PageSize", req.PageSize);
            parameters.Add("@PageNumber", req.PageNumber);

            var result = await QueryAsync<RequestResponsemodel>(StoreProcedures.GetShopAllRequest, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();

        }

        public async Task<RequestResponsemodel> GetCaseInfo(long caseId)
        {
            DynamicParameters dp = new();
            dp.Add("@caseId", caseId);
            var result = await QueryFirstOrDefaultAsync<RequestResponsemodel>(StoreProcedures.GetCaseInfo, dp, commandType: CommandType.StoredProcedure);

            if (!string.IsNullOrEmpty(result.CaseImage))
            {
                result.CaseImages = result.CaseImage.Split(',').ToList();
                result.CaseImage = null;

            }

            return result;
        }


    }
}


