 
using System.ComponentModel.DataAnnotations;
 
namespace Model.dbModels;

public partial class images
{
    [Key]
    public long Id { get; set; }

    public string ImageName { get; set; } = null!;

    public long ShopId { get; set; }

    public long RequestId { get; set; }
}