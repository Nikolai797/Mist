using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Data.Entities;

public class OrderItem
{
    [Key, Column(Order = 0)]
    public int OrderId { get; set; }   // ← ЛИПСВАЛО

    [Key, Column(Order = 1)]
    public int GameId { get; set; }    // ← ЛИПСВАЛО

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    // Навигации
    [ForeignKey(nameof(OrderId))]
    public virtual Order? Order { get; set; }

    [ForeignKey(nameof(GameId))]
    public virtual Game? Game { get; set; }
}