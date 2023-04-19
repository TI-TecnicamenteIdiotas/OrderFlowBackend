﻿namespace OrderFlow.Data.Models;

public partial class Category
{
    public Category()
    {
        Products = new HashSet<Product>();
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public int ColorTheme { get; set; }
    public int CategoryIcon { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}