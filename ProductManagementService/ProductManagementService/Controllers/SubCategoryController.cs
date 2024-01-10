using Microsoft.AspNetCore.Mvc;
using ProductManagement.Manager;
using ProductManagement.Models;

namespace ProductManagementService.Controllers
{
    /// <summary>
    /// Represents the sub category controller.
    /// </summary>
    [Route("api/subcategory")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        public readonly ILogger<CategoryController> _logger;
        public readonly ISubCategoryManager _subCategoryManager;
       
        /// <summary>
        /// The sub category controller.
        /// </summary>
        /// <param name="subCategoryManager">The sub category manager instance.</param>
        /// <param name="logger">The logger instance.</param>
        public SubCategoryController(ISubCategoryManager subCategoryManager, ILogger<CategoryController> logger)
        {
            _subCategoryManager = subCategoryManager;
            _logger = logger;
        }

        /// <summary>
        /// Method to get all sub categories.
        /// </summary>
        /// <returns>The sub categories.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<SubCategory> subCategories = await _subCategoryManager.GetAllSubCategories();
            var message = subCategories != null && subCategories.Any()
                        ? string.Format($"{subCategories.Count()} :SubCategories found")
                        : string.Format("SubCategories not found");

            _logger.LogInformation(message);
            return Ok(subCategories);
        }

        /// <summary>
        /// Method to get sub categories according to category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>The sub categories list.</returns>
        [HttpGet]
        [Route("{categoryId:int}")]
        public async Task<IActionResult> Get(int categoryId)
        {
            if (categoryId <= 0)
            {
                _logger.LogError(Constants.INVALIDCATEGORYEXCEPTIONMESSAGE);
                throw new InvalidCategoryIdentifierException();
            }

            List<SubCategory> subCategories = await _subCategoryManager.GetSubCategories(categoryId);
            var message = subCategories != null && subCategories.Any()
                        ? string.Format($"{subCategories.Count()} :SubCategories found")
                        : string.Format("SubCategories not found");

            _logger.LogInformation(message);
            return Ok(subCategories);
        }
    }
}
