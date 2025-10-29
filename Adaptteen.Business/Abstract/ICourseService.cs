using Adaptteen.Common.DTOs;
using Adaptteen.Common.Results;
using Adaptteen.DataAccess.Abstract;
using Adaptteen.DataAccess.Model;

namespace Adaptteen.Business.Abstract
{
    public interface ICourseService<TEntity> : IBaseDal<TEntity, Guid> where TEntity : Course
    {
        Task<IDataResult<CourseDto?>> Create(CourseDto dto);
        Task<IDataResult<CourseDto?>> Update(CourseDto dto);
        Task<IDataResult<CourseDto?>> Delete(CourseDto dto);
        Task<IDataResult<CourseData?>> GetAllCourses();
        Task<IDataResult<CourseDto?>> GetCourse(Guid id);
    }
}
