 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Model.dbModels;

public partial class UserType
{
    [Key]
    public long Id { get; set; }

    [Column("UserType")]
    [StringLength(100)]
    public string UserType1 { get; set; } = null!;

    [StringLength(100)]
    public string Displayname { get; set; } = null!;
}