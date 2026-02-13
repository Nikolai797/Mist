using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    
    public virtual ICollection<Game>? Games { get; set; }   
}