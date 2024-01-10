using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagement.Manager;
using ProductManagement.Models;

namespace ProductManagementService.Controllers
{
    /// <summary>
    /// Represents Category controller calls. 
    /// </summary>
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ILogger<CategoryController> _logger;
        public readonly ICategoryManager _categoryManager;

        /// <summary>
        /// The category controller constructor.
        /// </summary>
        /// <param name="categoryManager">The category manager.</param>
        /// <param name="logger">The logger instance.</param>
        public CategoryController(ICategoryManager categoryManager, ILogger<CategoryController> logger)
        {
            _categoryManager = categoryManager;
            _logger = logger;
        }

        /// <summary>
        /// Method to get all categories.
        /// </summary>
        /// <returns>The all categories.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = await _categoryManager.GetCategories();
            var message = categories != null && categories.Any()
                        ? string.Format($"{categories.Count()} :categories found")
                        : string.Format("Categories not found");

            _logger.LogInformation(message);
            return Ok(categories);
        }
    }
}
