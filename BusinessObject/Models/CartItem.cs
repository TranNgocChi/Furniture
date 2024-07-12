using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool Selected { get; set; }

        public virtual required Product Product { get; set; }
        public virtual required Cart Cart { get; set; }

    }
}
