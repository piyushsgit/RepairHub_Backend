using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonMethods
{
    public class NonStaticCommonMethods : INonStaticCommonMethods
    {
        private readonly IConfiguration iConfig;

        public NonStaticCommonMethods(IConfiguration iConfig)

        {
            this.iConfig = iConfig;
        }

        public IConfigurationSection? GetConfigurationSection(string SectionName)
        {
            return iConfig.GetSection(SectionName) ?? null;

        }

        public string? GetConfigurationValue(string Key)
        {
            return iConfig.GetValue<string>(Key) ?? null;

        }

    }
}
