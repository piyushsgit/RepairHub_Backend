using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonMethods
{
     public interface INonStaticCommonMethods
    {
        string GetConfigurationValue(string Key);
    }
}
