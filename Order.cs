using System.ComponentModel.DataAnnotations;
using GameStore.Data.Identity;   

namespace GameStore.Data.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

   
    public virtual ApplicationUser? User { get; set; }

    public virtual ICollection<OrderItem>? OrderItems { get; set; }
}