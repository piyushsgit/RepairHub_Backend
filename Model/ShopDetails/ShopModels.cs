using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.ShopDetails
{
    public class ShopModels
    {
        public class ShopDetails
        { 
            public long UserId { get; set; }
            [StringLength(255)]
            public string ShopName { get; set; } = null!;
            [StringLength(100)]
            public string ShopOwnerName { get; set; } = null!;
            [StringLength(100)]
            public string ContactNumber { get; set; } = null!;
            public string AddharNumber { get; set; }
            public string PanNumber { get; set; } = null!;
            public string ShopDescription { get; set; } = null!;
            [Column(TypeName = "decimal(5, 2)")]
            public decimal? Rating { get; set; }
            public int ShopReparingType { get; set; }
            public string Address { get; set; } = null!;
            [Column(TypeName = "datetime")]
            public DateTime Since { get; set; } 
            public string AsociateWith { get; set; } = null!;
            public long Id { get; set; }
            [StringLength(255)]
            public string? ShopType { get; set; }
        }

      
    }
}
