using Adaptteen.Business.Abstract;
using Adaptteen.Common.Constants.HttpRequestUrls;
using Adaptteen.Common.DTOs;
using Adaptteen.Common.Results;
using Adaptteen.Common.Validations.Abstract;
using Adaptteen.DataAccess.Model;
using Microsoft.AspNetCore.Mvc;

namespace Adaptteen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService<Course> _service;
        private readonly IModelStateResponseService _modelStateResponseService;

        public CourseController(ICourseService<Course> service, IModelStateResponseService modelStateResponseService)
        {
            _service = service;
            _modelStateResponseService = modelStateResponseService;
        }

        [HttpPost]
        [Route($"~{CourseModuleHttpRequestUrl.CreateCourseUrl}")]
        public async Task<IDataResult<CourseDto?>> Create([FromBody] CourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CourseDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.Create(dto);
        }

        [HttpPut]
        [Route($"~{CourseModuleHttpRequestUrl.UpdateCourseUrl}")]
        public async Task<IDataResult<CourseDto?>> Update([FromBody] CourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CourseDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.Update(dto);
        }

        [HttpPut]
        [Route($"~{CourseModuleHttpRequestUrl.DeleteCourseUrl}")]
        public async Task<IDataResult<CourseDto?>> Delete(CourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CourseDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.Delete(dto);
        }

        [HttpGet]
        [Route($"~{CourseModuleHttpRequestUrl.GetByIdCourseUrl}")]
        public async Task<IDataResult<CourseDto?>> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CourseDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.GetCourse(id);
        }

        [HttpGet]
        [Route($"~{CourseModuleHttpRequestUrl.ListCourseUrl}")]
        public async Task<IDataResult<CourseData?>> GetAllCourses()
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<CourseData?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _service.GetAllCourses();
        }
    }
}
