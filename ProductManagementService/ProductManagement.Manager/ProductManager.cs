using Microsoft.Extensions.Logging;
using ProductManagement.Models;
using ProductManagement.Repository;

namespace ProductManagement.Manager
{
    /// <summary>
    /// Represents product manager class.
    /// </summary>
    public class ProductManager : IProductManager
    {
        public readonly IProductRepository _productRespository;
        public readonly ILogger<ProductManager> _logger;

        /// <summary>
        /// The product manager constructor.
        /// </summary>
        /// <param name="productRespository">The product repository instance.</param>
        /// <param name="logger">The logger instance.</param>
        public ProductManager(IProductRepository productRespository, ILogger<ProductManager> logger)
        {
            _logger = logger;
            _productRespository = productRespository;
        }

        /// <inheritdoc>
        public async Task<List<Product>> GetProducts()
        {
            _logger.LogInformation($"Receieved:GetProducts request in product manager.");
            return await _productRespository.GetProducts();
        }

        /// <inheritdoc>
        public async Task<List<Product>> GetProductsBySubCategoryId(int subcategoryId)
        {
            _logger.LogInformation($"Receieved: request in product manager with subcategory identifier: {subcategoryId}");
            return await _productRespository.GetProductsBySubCategoryId(subcategoryId);
        }

        /// <inheritdoc>
        public async Task<bool> AddProduct(Product product)
        {
            // Check if product already exists in database.
            if (await IsDuplicate(product))
            {
                throw new DuplicateProductException();
            }

            _logger.LogInformation("Receieved: request in product manager.");
            return await _productRespository.AddProduct(product);
        }

        /// <inheritdoc>
        public async Task<bool> UpdateProduct(Product product, int productId)
        {
            _logger.LogInformation($"Receieved: request in manager with product identifier: {productId}");
            if (await _productRespository.GetProductById(productId) != null)
            {
                _logger.LogInformation($"Product found for specified product identifier: {productId}");
                return await _productRespository.UpdateProduct(product, productId);
            }
            else
            {
                _logger.LogError($"Product not found for specified product identifier: {productId}");
                throw new ProductNotFoundException();
            }
        }

        private async Task<bool> IsDuplicate(Product product)
        {
            var retrievedProducts = await _productRespository.GetProductsBySubCategoryId(product.SubCategory.Id);
            if (retrievedProducts != null)
            {
                if (retrievedProducts.Any(item => item.ProductCode == product.ProductCode))
                {
                    _logger.LogError(Constants.DUPLICATEPRODUCTEXCEPTIONMESSAGE);
                    return true;
                }
            }

            return false;
        }
    }
}