using System;
using System.Collections.Generic;

namespace ProductManagement.Repository.Models;

public partial class SubCategoryRecord
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int CategoryId { get; set; }

    public virtual CategoryRecord Category { get; set; } = null!;

    public virtual ICollection<ProductRecord> Products { get; set; } = new List<ProductRecord>();
}
