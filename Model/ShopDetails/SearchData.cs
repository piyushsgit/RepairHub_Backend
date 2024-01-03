using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ShopDetails
{
    public class SearchData
    {
        public int Id { get; set; }

        public string ShopName { get; set; }
        public string ShopOwnerName { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string ShopDescription { get; set; } = null!;
        public int Rating { get; set; }
        public string? ShopTypes { get; set; }
        [StringLength(100)]

        public string Country { get; set; } = null!;

        [StringLength(100)]

        public string State { get; set; } = null!;

        [StringLength(100)]

        public string City { get; set; } = null!;
        public string Address { get; set; }
        public string EncryptshopId { get; set; }

        [StringLength(100)]
        public string Area { get; set; }
    }

    
}
