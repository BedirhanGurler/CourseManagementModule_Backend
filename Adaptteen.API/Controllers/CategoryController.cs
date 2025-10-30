using Adaptteen.Business.Abstract;
using Adaptteen.Common.Constants.HttpRequestUrls;
using Adaptteen.Common.DTOs;
using Adaptteen.Common.Results;
using Adaptteen.Common.Validations.Abstract;
using Adaptteen.DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adaptteen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService<Category> _service;
        private readonly IModelStateResponseService _modelStateResponseService;

        public CategoryController(ICategoryService<Category> service, IModelStateResponseService modelStateResponseService)
        {
            _service = service;
            _modelStateResponseService = modelStateResponseService;
        }

        [HttpPost]
        [Route($"~{CourseModuleHttpRequestUrl.CreateCategoryUrl}")]
        public async Task<IDataResult<CategoryDto?>> Create([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CategoryDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.Create(dto);
        }

        [HttpPut]
        [Route($"~{CourseModuleHttpRequestUrl.UpdateCategoryUrl}")]
        public async Task<IDataResult<CategoryDto?>> Update([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CategoryDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.Update(dto);
        }

        [HttpPut]
        [Route($"~{CourseModuleHttpRequestUrl.DeleteCategoryUrl}")]
        public async Task<IDataResult<CategoryDto?>> Delete(CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CategoryDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.Delete(dto);
        }

        [HttpPut]
        [Route($"~{CourseModuleHttpRequestUrl.ActivateCategoryUrl}")]
        public async Task<IDataResult<CategoryDto?>> Activate(CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CategoryDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.Activate(dto);
        }

        [HttpGet]
        [Route($"~{CourseModuleHttpRequestUrl.GetByIdCategoryUrl}")]
        public async Task<IDataResult<CategoryDto?>> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CategoryDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.GetCategory(id);
        }

        [HttpGet]
        [Route($"~{CourseModuleHttpRequestUrl.ListCategoryUrl}")]
        public async Task<IDataResult<CategoryData?>> GetAllCategories()
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CategoryData?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.GetAllCategories();
        }
    }
}
