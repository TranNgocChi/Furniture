﻿using FurnitureApp.Models;

namespace DataAccess.Repository.IRepository;

public interface IOrderItemRepository
{
	public void Create(OrderItem orderItem);
}
