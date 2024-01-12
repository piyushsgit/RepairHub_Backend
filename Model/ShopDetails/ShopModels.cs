using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

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
            public string? ShopReparingType { get; set; }
            public string Address { get; set; } = null!;
            [Column(TypeName = "datetime")]
            public DateTime Since { get; set; } 
            public string AsociateWith { get; set; } = null!;
            public long Id { get; set; }
            [StringLength(255)]
            public string? ShopType { get; set; }

        }
        public class ShopDetailsById
        {
            public long UserId { get; set; }
            [StringLength(255)]
          
            public string ShopName { get; set; } = null!;
            [StringLength(100)]
            public string ShopOwnerName { get; set; } = null!;
            [StringLength(100)]
            public string EncryptedId { get; set; } = null!;
            [StringLength(100)]
            public string ContactNumber { get; set; } = null!;
            public string ShopDescription { get; set; } = null!;
            [Column(TypeName = "decimal(5, 2)")]
            public decimal? Rating { get; set; }

            [Column(TypeName = "decimal(12, 9)")]
            public decimal? latitude { get; set; }
            [Column(TypeName = "decimal(12, 9)")]
            public decimal? longitude { get; set; }

            public string? ShopReparingType { get; set; }
            public string FullAddress { get; set; } = null!;
            [Column(TypeName = "datetime")]
            public DateTime Since { get; set; }
            public string AsociateWith { get; set; } = null!;
            public string? ShopTypeNames { get; set; }
            [JsonIgnore]
            [XmlIgnore]
            public string? ImageNames { get; set; } = null!; 
            public Image[] Images { get; set; } = Array.Empty<Image>();
            public long Id { get; set; }
            [StringLength(255)] 
            public string? DistanceInKm { get; set; }
        } 

        public class Location
        {
            [DefaultValue(null)]
            public decimal? latitude { get; set; }  
            [Column(TypeName = "decimal(12, 9)")]
            [DefaultValue(null)]
            public decimal? longitude { get; set; } 
            [Column(TypeName = "decimal(12, 9)")]
            [DefaultValue(null)]
            public float? RadiusInKms { get; set; }  
        }

        public class Image
        {
            public string ImageName { get; set; }
        }
    }
}
