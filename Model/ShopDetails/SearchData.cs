using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ShopDetails
{
    public class SearchData
    {
        public int ShopId { get; set; }

        public string ShopName { get; set; }
        public string ShopOwnerName { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string ShopDescription { get; set; } = null!;
        public int Rating { get; set; }
        public string? ShopTypes { get; set; }
    }
}
