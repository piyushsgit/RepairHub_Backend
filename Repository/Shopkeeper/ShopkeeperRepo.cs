using Common.CommonMethods;
using Common.Helper;
using Dapper;
using Data;
using System.Data;
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
    }
}
    
