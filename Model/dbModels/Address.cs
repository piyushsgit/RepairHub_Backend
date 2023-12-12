
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.dbModels;

public partial class Address
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [StringLength(100)]
   
    public string Country { get; set; } = null!;

    [StringLength(100)]
   
    public string State { get; set; } = null!;

    [StringLength(100)]
   
    public string City { get; set; } = null!;

   
    public string Area { get; set; } = null!;

    [StringLength(255)]
   
    public string? AddressType { get; set; }

    public bool? IsShopAddress { get; set; }

    public bool? IsDelete { get; set; }

    public long? DeletedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedOn { get; set; }

    public long? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}