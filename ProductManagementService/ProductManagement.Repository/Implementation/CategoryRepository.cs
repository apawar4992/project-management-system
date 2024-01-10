using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManagement.Models;
using ProductManagement.Repository.Models;

namespace ProductManagement.Repository
{
    /// <summary>
    /// Represents category repository.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        public readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(ILogger<CategoryRepository> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc>
        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new List<Category>();
            List<CategoryRecord> categoryRecords;
            using (ProductManagementSystemContext context = new ProductManagementSystemContext())
            {
                categoryRecords = await context.Categories.ToListAsync();
            }

            if (categoryRecords != null)
            {
                _logger.LogInformation($"Reteieved {categoryRecords.Count()} : categories from database.");
                categoryRecords.ForEach(item =>
                {
                    categories.Add(new Category()
                    {
                        Name = item.Name,
                        Id = item.Id,
                    });
                });
            }

            return categories;
        }
    }
}
