namespace FurnitureApp.Models.Cart
{
    public class CartDto
    {
        public decimal CartTotal { get; set; }
        public string? UserName { get; set; }
        public CartItemDto[]? CartItemDtos { get; set; }
    }

    public class CartItemDto
    {
        public int Quantity { get; set; }
        public string? ProductJson { get; set; }
    }
}
