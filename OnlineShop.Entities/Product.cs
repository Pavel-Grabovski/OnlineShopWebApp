﻿namespace OnlineShop.Entities;

public class Product
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public decimal Cost { get; set; }
	public string? Description { get; set; }
	public List<Image> Images { get; set; }
}
