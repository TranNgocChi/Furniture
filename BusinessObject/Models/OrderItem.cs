﻿using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models;

public class OrderItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Product Product { get; set; }

    [Range(0, 9999999999)]
    public int Quantity { get; set; }

}
