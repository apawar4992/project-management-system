using ProductManagement.Models;

namespace ProductManagement.Manager
{
    /// <summary>
    /// Represents the category manager interface.
    /// </summary>
    public interface ICategoryManager
    {
        /// <summary>
        /// The get categories.
        /// </summary>
        /// <returns>Returns, list of categories.</returns>
        Task<List<Category>> GetCategories();
    }
}
