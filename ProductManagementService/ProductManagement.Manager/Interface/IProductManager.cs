using ProductManagement.Models;

namespace ProductManagement.Manager
{
    /// <summary>
    /// Represents the product manager interface.
    /// </summary>
    public interface IProductManager
    {
        /// <summary>
        /// Method to get all products.
        /// </summary>
        /// <returns>Returns, the list of products.</returns>
        Task<List<Product>> GetProducts();

        /// <summary>
        /// Method to get products by sub category identifier.
        /// </summary>
        /// <param name="subcategoryId">The sub category identifier.</param>
        /// <returns>Returns, the list of products.</returns>
        Task<List<Product>> GetProductsBySubCategoryId(int subcategoryId);

        /// <summary>
        /// Method to add product.
        /// </summary>
        /// <param name="product">The product instance.</param>
        /// <returns>Returns, true if product added successfully.</returns>
        Task<bool> AddProduct(Product product);

        /// <summary>
        /// Method to update product.
        /// </summary>
        /// <param name="product">The product instance.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Returns, true if product updated successfully.</returns>
        Task<bool> UpdateProduct(Product product, int productId);
    }
}
