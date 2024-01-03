using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UsersModels
{
    public class GetAddress
    {
        public long Id { get; set; }
        public string? FullAddress { get; set; }
    }

    public class AddressInsertModel
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public long? ShopId { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Area { get; set; }
        public string? AddressType { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Decription { get; set; }
    }


}
