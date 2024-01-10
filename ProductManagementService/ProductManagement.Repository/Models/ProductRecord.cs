using System;
using System.Collections.Generic;

namespace ProductManagement.Repository.Models;

public partial class ProductRecord
{
    public int Id { get; set; }

    public string ProductCode { get; set; } = null!;

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public int? Price { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public int? SubCategoryId { get; set; }

    public virtual SubCategoryRecord? SubCategory { get; set; }
}
