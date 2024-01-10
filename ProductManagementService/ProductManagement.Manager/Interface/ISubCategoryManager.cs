using ProductManagement.Models;

namespace ProductManagement.Manager
{
    /// <summary>
    /// Represents the sub category manager interface.
    /// </summary>
    public interface ISubCategoryManager
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
