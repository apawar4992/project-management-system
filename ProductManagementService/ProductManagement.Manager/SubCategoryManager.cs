using Microsoft.Extensions.Logging;
using ProductManagement.Models;
using ProductManagement.Repository;

namespace ProductManagement.Manager
{
    /// <summary>
    /// Represents the sub category manager.
    /// </summary>
    public class SubCategoryManager : ISubCategoryManager
    {
        public readonly ILogger<SubCategoryManager> _logger;
        public readonly ISubCategoryRepository _subCategoryRepository;

        /// <summary>
        /// The sub category manager constructor.
        /// </summary>
        /// <param name="subCategoryRepository">The sub category repository instance.</param>
        /// <param name="logger">The logger instance.</param>
        public SubCategoryManager(ISubCategoryRepository subCategoryRepository, ILogger<SubCategoryManager> logger)
        {
            _subCategoryRepository = subCategoryRepository;
            _logger = logger;
        }

        /// <inheritdoc>
        public async Task<List<SubCategory>> GetAllSubCategories()
        {
            _logger.LogInformation($"Receieved: request in get all sub categories manager.");
            return await _subCategoryRepository.GetAllSubCategories();
        }

        /// <inheritdoc>
        public async Task<List<SubCategory>> GetSubCategories(int categoryId)
        {
            _logger.LogInformation($"Receieved: request in sub category manager with category identifier: {categoryId}");
            return await _subCategoryRepository.GetSubCategories(categoryId);
        }
    }
}
