using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonMethods
{
     public interface INonStaticCommonMethods
    {
        IConfigurationSection? GetConfigurationSection(string SectionName);
        string GetConfigurationValue(string Key);
    }
}
