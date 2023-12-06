 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Model.dbModels;

public partial class ShopDetails
{
    [Key]
    public long Id { get; set; }

    [StringLength(255)]
   
    public string ShopName { get; set; } = null!;

    [StringLength(100)]
   
    public string ShopOwnerName { get; set; } = null!;

    [StringLength(100)]
   
    public string ContactNumber { get; set; } = null!;

    public int AddharNumber { get; set; }

    [StringLength(50)]
   
    public string PanNumber { get; set; } = null!;

   
    public string ShopDescription { get; set; } = null!;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Rating { get; set; }

    public int ShopReparingType { get; set; }

    public long? ShopAddress { get; set; }

    public bool? IsDelete { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Since { get; set; }

    public long? DeletedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedOn { get; set; }

    public long? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

   
    public string AsociateWith { get; set; } = null!;
}