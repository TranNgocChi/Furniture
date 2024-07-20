using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models.Product
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name Product is required")]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Description Product is required")]
        [StringLength(1000)]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Price Product is required")]
        [Range(0, 9999999999, ErrorMessage = "Price can not exceed 10 digits")]
        public decimal ProductPrice { get; set; }

        [Range(0, 9999999999, ErrorMessage = "Quantity can not exceed 10 digits")]
        public int Quantity { get; set; }

        [StringLength(200, ErrorMessage = "Product Image Length <= 200")]
        public string? ProductImage { get; set; }

        [Required(ErrorMessage = "Product must have suitable category")]
        public string CategoryId { get; set; }
    }
}
