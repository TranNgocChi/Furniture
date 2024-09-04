using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public decimal ShippingPrice { get; set; }

    [Required]
    public decimal OrderTotal { get; set; }

    [Required]
    public required Address OrderAddress { get; set; }

    [Required]
    public required User? UserOrder { get; set; }

    [Required]
    public string? PaymentMethod { get; set; }

    [StringLength(100, ErrorMessage = "Can't be over 100 characters")]
    public string? OrderNote { get; set; }

    [Required(ErrorMessage = "Order Date Is Required")]
    public DateTime OrderDate { get; set; }
    public Status? Status { get; set; }

    [Required(ErrorMessage = "Order Items can't be null")]
    public required ICollection<OrderItem> OrderItems { get; set; }
}
