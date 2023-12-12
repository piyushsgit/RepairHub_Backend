using Common.CommonMethods;
using Common.Helper;
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
    }  
}
    
