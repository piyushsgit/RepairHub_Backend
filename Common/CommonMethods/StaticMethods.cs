using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.AppSettingsJason;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonMethods
{
    public  class StaticMethods
    {

        public static async Task<string> SaveImageAsync(IFormFile image, string uploadsFolder)
        {
            if (image == null)
            {
                return null;
            }

            string uniqueFileName = Guid.NewGuid() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return uniqueFileName;
        }


    }
}
