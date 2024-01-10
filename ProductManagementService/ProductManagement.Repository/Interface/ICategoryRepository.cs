using ProductManagement.Models;

namespace ProductManagement.Repository
{
    /// <summary>
    /// Represents the category repository interface.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// The get categories.
        /// </summary>
        /// <returns>Returns, list of categories.</returns>
        Task<List<Category>> GetCategories();
    }
}
