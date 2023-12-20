using Common.CommonMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RegisterCommon
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dataDictionary = new Dictionary<Type, Type>
            {
                 {typeof(INonStaticCommonMethods), typeof(NonStaticCommonMethods)}
            };
            return dataDictionary;
        }
    }
}
