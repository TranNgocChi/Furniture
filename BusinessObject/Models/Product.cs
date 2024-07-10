using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name Product is required")]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Description Product is required")]
        [StringLength(1000)]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Price Product is required")]
        [StringLength(10, ErrorMessage = "Price has length <= 10 ")]
        public decimal ProductPrice { get; set; }

        [StringLength(10)]
        public int Quantity { get; set; }

        [StringLength(200, ErrorMessage = "Product Image Length <= 200")]
        public string? ProductImage { get; set; }

        [Required(ErrorMessage = "Product must have suitable category")]
        public virtual required Category Category { get; set; }

    }
}
