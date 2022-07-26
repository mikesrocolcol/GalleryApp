
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inventory.Models;

public class ProductEntities
{
    [Key]
    public int Id { get; set; }

 
   
   
    public string? Image { get; set; }

    [NotMapped]
    [Display(Name = "File")]
    public IFormFile? FormFile { get; set; }

  
}



