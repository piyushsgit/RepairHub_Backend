using Microsoft.AspNetCore.Http;
using Model.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UsersModels
{
    public class InsertRequestmodel
    {
        public long UserId { get; set; }
        public long ShopId { get; set; }
        public string? Description { get; set; }
        public long UserAddressId { get; set; }
        public DateTime? CreatedBy { get; set; }
        public IFormFile[]? RequestImage { get; set; }
        public List<string>? RequestImageName { get; set; }
    }
    public class InsertRequestResponsemodel
    {
        public long RequestId { get; set; }
       
    }
}
