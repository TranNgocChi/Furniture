using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public decimal CartTotal { get; set; }

        public virtual required User UserCart { get; set; }
    }
}
