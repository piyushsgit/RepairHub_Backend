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
        public string? UserId { get; set; }
        public string? ShopId { get; set; }
        public string? Description { get; set; }
        public string? Tittle { get; set; }
        public long? UserAddressId { get; set; }
       
        public List<string>? RequestImageName { get; set; }
    }
    public class InsertRequestResponsemodel
    {
        public long RequestId { get; set; }
       
    }
}
