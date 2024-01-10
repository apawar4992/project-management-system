using Microsoft.Extensions.Logging;
using ProductManagement.Models;
using ProductManagement.Repository;

namespace ProductManagement.Manager
{
    /// <summary>
    /// Represents category manager class.
    /// </summary>
    public class CategoryManager : ICategoryManager
    {
        public readonly ILogger<CategoryManager> _logger;
        public readonly ICategoryRepository _categoryRepository;
        public readonly ISubCategoryManager _subCategoryManager;

        /// <summary>
        /// The category manager constructor.
        /// </summary>
        /// <param name="categoryRepository">The category repository instance.</param>
        /// <param name="logger">The logger instance.</param>
        public CategoryManager(ICategoryRepository categoryRepository, ISubCategoryManager subCategoryManager, ILogger<CategoryManager> logger)
        {
            _categoryRepository = categoryRepository;
            _subCategoryManager = subCategoryManager;
            _logger = logger;
        }

        /// <inheritdoc>
        public async Task<List<Category>> GetCategories()
        {
            _logger.LogInformation($"Receieved: request in category manager.");
            var categories = await _categoryRepository.GetCategories();

            // Get sub categories.
            if (categories != null && categories.Any())
            {
                categories?.ForEach(async categoryItem =>
                {
                    categoryItem.subCategories = await _subCategoryManager.GetSubCategories(categoryItem.Id);
                });
            }

            return categories;
        }
    }
}
