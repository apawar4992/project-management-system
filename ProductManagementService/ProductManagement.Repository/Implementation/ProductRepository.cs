using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManagement.Models;
using ProductManagement.Repository.Models;

namespace ProductManagement.Repository
{
    public class ProductRepository : IProductRepository
    {
        public readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ILogger<ProductRepository> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc>
        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = new List<Product>();
            List<ProductRecord> productRecords;
            using (ProductManagementSystemContext context = new ProductManagementSystemContext())
            {
                productRecords = await context.Products.ToListAsync();
            }

            if (productRecords != null)
            {
                _logger.LogInformation($"{productRecords.Count()}: products found.");
                productRecords.ForEach(item =>
                {
                    products.Add(new Product()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ProductCode = item.ProductCode,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Description = item.Description,
                        Image = item.Image,
                        SubCategory = new SubCategory()
                        {
                            Id = (int)item?.SubCategoryId
                        }
                    });
                });
            }

            return products;
        }

        /// <inheritdoc>
        public async Task<List<Product>> GetProductsBySubCategoryId(int subCategoryId)
        {
            List<Product> products = new List<Product>();
            List<ProductRecord> productRecords;
            using (ProductManagementSystemContext context = new ProductManagementSystemContext())
            {
                productRecords = await context.Products.Where(item => item.SubCategoryId == subCategoryId).ToListAsync();
            }

            if (productRecords != null)
            {
                _logger.LogInformation($"{productRecords.Count()}: Products Found for specified subCategory Id:{subCategoryId}");
                productRecords.ForEach(item =>
                {
                    products.Add(new Product()
                    {
                        Name = item.Name,
                        ProductCode = item.ProductCode,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Description = item.Description,
                        Image = item.Image,
                        Id = item.Id,
                        SubCategory = new SubCategory()
                        {
                            Id = (int)item?.SubCategoryId
                        }
                    });
                });
            }

            return products;
        }

        /// <inheritdoc>
        public async Task<Product> GetProductById(int productId)
        {
            Product product = new Product();
            ProductRecord retrievedProduct;
            using (ProductManagementSystemContext context = new ProductManagementSystemContext())
            {
                retrievedProduct = await context.Products.FirstOrDefaultAsync(item => item.Id == productId);
            }

            if (retrievedProduct != null)
            {
                _logger.LogInformation($"Product Found for specified product Id:{productId}");
                product = new Product()
                {
                    Name = retrievedProduct.Name,
                    ProductCode = retrievedProduct.ProductCode,
                    Price = retrievedProduct.Price,
                    Quantity = retrievedProduct.Quantity,
                    Description = retrievedProduct.Description,
                    Image = retrievedProduct.Image,
                    SubCategory = new SubCategory()
                    {
                        Id = (int)retrievedProduct?.SubCategoryId
                    }
                };
            }

            return product;
        }

        /// <inheritdoc>
        public async Task<bool> AddProduct(Product product)
        {
            ProductRecord record = new ProductRecord()
            {
                Name = product.Name,
                ProductCode = product.ProductCode,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                Image = product.Image,
                SubCategoryId = product.SubCategory.Id
            };
            using (ProductManagementSystemContext context = new ProductManagementSystemContext())
            {
                await context.Products.AddAsync(record);
                await context.SaveChangesAsync();
            }

            _logger.LogInformation("Product added successfully.");
            return true;
        }

        /// <inheritdoc>
        public async Task<bool> UpdateProduct(Product product, int productId)
        {
            using (ProductManagementSystemContext context = new ProductManagementSystemContext())
            {
                var retrievedProduct = await context.Products.FindAsync(productId);
                if (retrievedProduct != null)
                {
                    _logger.LogInformation($"Found product with specified product id:{productId}");

                    retrievedProduct.Name = product.Name;
                    retrievedProduct.ProductCode = product.ProductCode;
                    retrievedProduct.Price = product.Price;
                    retrievedProduct.Quantity = product.Quantity;
                    retrievedProduct.Description = product.Description;
                    retrievedProduct.Image = product.Image;
                    retrievedProduct.SubCategoryId = product.SubCategory?.Id;

                    await context.SaveChangesAsync();
                }
                else
                    return false;
            }

            return true;
        }
    }
}