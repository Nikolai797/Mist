using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Data.Entities;

public class Game
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }


    public int? CategoryId { get; set; }


    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderItem>? OrderItems { get; set; }
}