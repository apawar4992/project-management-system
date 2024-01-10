using ProductManagement.Models;

namespace ProductManagement.Repository
{
    /// <summary>
    /// Represents the sub category repository interface.
    /// </summary>
    public interface ISubCategoryRepository
    {
        /// <summary>
        /// Method to get all sub categories.
        /// </summary>
        /// <returns>The list of sub categories.</returns>
        Task<List<SubCategory>> GetAllSubCategories();

        /// <summary>
        /// Method to get all sub categories.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>The list of sub categories.</returns>
        Task<List<SubCategory>> GetSubCategories(int categoryId);
    }
}
