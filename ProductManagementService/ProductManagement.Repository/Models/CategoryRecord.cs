using System;
using System.Collections.Generic;

namespace ProductManagement.Repository.Models;

public partial class CategoryRecord
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<SubCategoryRecord> SubCategories { get; set; } = new List<SubCategoryRecord>();
}
