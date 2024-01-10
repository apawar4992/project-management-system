using Microsoft.AspNetCore.Mvc;
using ProductManagement.Manager;
using ProductManagement.Models;

namespace ProductManagementService.Controllers
{
    /// <summary>
    /// Product controller class representing CURD operations.
    /// </summary>
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        public readonly ILogger<ProductController> _logger;
        public readonly IProductManager _productManager;

        /// <summary>
        /// Product controller constructor.
        /// </summary>
        /// <param name="productManager">The product manager.</param>
        /// <param name="logger">The logger instance.</param>
        public ProductController(IProductManager productManager, ILogger<ProductController> logger)
        {
            _logger = logger;
            _productManager = productManager;
        }

        /// <summary>
        /// Method to get all products.
        /// </summary>
        /// <returns>The action result of products.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Product> products = await _productManager.GetProducts();
            if (products != null && products.Any())
                _logger.LogInformation($"The products found.");

            return Ok(products);
        }

        /// <summary>
        /// Method to get products according to specified sub category identifier.
        /// </summary>
        /// <param name="subCategoryId">The sub category identifier.</param>
        /// <returns>The action result of products.</returns>
        [HttpGet]
        [Route("{subCategoryId:int}")]
        public async Task<IActionResult> Get(int subCategoryId)
        {
            List<Product> products;
            if (subCategoryId <= 0)
            {
                _logger.LogError(Constants.INVALIDSUBCATEGORYEXCEPTIONMESSAGE);
                throw new InvalidSubCategoryIdentifierException();
            }

            // call
            products = await _productManager.GetProductsBySubCategoryId(subCategoryId);

            var message = products != null && products.Count() > 0
                        ? string.Format($"The products for specified sub category identifier: {subCategoryId} found")
                        : string.Format($"The products for specified sub category identifier: {subCategoryId} not found");
            _logger.LogInformation(message);
            return Ok(products);
        }

        /// <summary>
        /// Method to add product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>The action result of is added.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (product == null)
            {
                _logger.LogError(Constants.INVALIDINPUTPARAMETER);
                throw new ArgumentNullException();
            }

            var isAdded = await _productManager.AddProduct(product);
            _logger.LogInformation("Products added successfully.");
            return Ok(isAdded);
        }

        /// <summary>
        /// Method to update the product according to product identifier.
        /// </summary>
        /// <param name="product">The product instance.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns>The action result of is updated.</returns>
        [HttpPut]
        [Route("{productId:int}")]
        public async Task<IActionResult> Update([FromBody] Product product, int productId)
        {
            if (product == null || productId <= 0)
            {
                _logger.LogError(Constants.INVALIDINPUTPARAMETER);
                throw new ArgumentNullException();
            }

            var isUpdated = await _productManager.UpdateProduct(product, productId);
            if (isUpdated)
                _logger.LogInformation($"Products updated successfully with specified product identifier:{productId}");
            return Ok(isUpdated);
        }
    }
}
