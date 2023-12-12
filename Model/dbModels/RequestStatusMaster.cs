using System.ComponentModel.DataAnnotations;  
namespace Model.dbModels;

public partial class RequestStatusMaster
{
    [Key]
    public long Id { get; set; }

    [StringLength(50)]
   
    public string StatusName { get; set; } = null!;

    [StringLength(100)]
   
    public string DisplayName { get; set; } = null!;
}