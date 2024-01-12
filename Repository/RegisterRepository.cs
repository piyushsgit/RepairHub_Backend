
using Repository.ManageRequest;
using Repository.Shopkeeper;
using Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RegisterRepository
    {
        public static Dictionary<Type,Type> GetTypes()
        {
            var types = new Dictionary<Type,Type>
            {
                { typeof(IUserRepository), typeof(UserRepository)},
                { typeof(IShopkeeperRepo), typeof(ShopkeeperRepo)},
                {typeof(IManageRequestRepo), typeof(ManageRequestRepo)},
            };
            return types;
        }
    }
}
