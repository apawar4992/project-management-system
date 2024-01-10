using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManagement.Models;
using ProductManagement.Repository.Models;

namespace ProductManagement.Repository
{
    /// <summary>
    /// Represents sub category repository.
    /// </summary>
    public class SubCategoryRepository : ISubCategoryRepository
    {
        public readonly ILogger<SubCategoryRepository> _logger;

        /// <summary>
        /// The sub category respository constructor.
        /// </summary>
        /// <param name="logger">The SubCategoryRepository logger instance.</param>
        public SubCategoryRepository(ILogger<SubCategoryRepository> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc>
        public async Task<List<SubCategory>> GetAllSubCategories()
        {
            List<SubCategory> categories = new List<SubCategory>();
            List<SubCategoryRecord> subCategoryRecords;
            using (ProductManagementSystemContext context = new ProductManagementSystemContext())
            {
                subCategoryRecords = await context.SubCategories.ToListAsync();
            }

            if (subCategoryRecords != null)
            {
                _logger.LogInformation($"Reteieved {subCategoryRecords} sub categories.");
                subCategoryRecords.ForEach(item =>
                {
                    categories.Add(new SubCategory()
                    {
                        Name = item.Name,
                        Id = item.Id,
                        CategoryId = item.CategoryId
                    });
                });
            }

            return categories;
        }

        /// <inheritdoc>
        public async Task<List<SubCategory>> GetSubCategories(int categoryId)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            List<SubCategoryRecord> subCategoryRecords;
            using (ProductManagementSystemContext context = new ProductManagementSystemContext())
            {
                subCategoryRecords = await context.SubCategories.Where(x => x.CategoryId == categoryId).ToListAsync();
            }

            if (subCategoryRecords != null)
            {
                _logger.LogInformation($"Reteieved {subCategoryRecords} sub categories with speificed category id: {categoryId}.");
                subCategoryRecords.ForEach(item =>
                {
                    subCategories.Add(new SubCategory()
                    {
                        Name = item.Name,
                        Id = item.Id,
                        CategoryId = categoryId
                    });
                });
            }

            return subCategories;
        }
    }
}
