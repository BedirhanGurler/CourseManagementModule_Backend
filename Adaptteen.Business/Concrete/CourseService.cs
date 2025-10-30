using Adaptteen.Business.Abstract;
using Adaptteen.Common.Constants.ResponseMessages;
using Adaptteen.Common.DTOs;
using Adaptteen.Common.Enums;
using Adaptteen.Common.Results;
using Adaptteen.DataAccess.Concrete;
using Adaptteen.DataAccess.Context;
using Adaptteen.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Adaptteen.Business.Concrete
{
    public class CourseService<TEntity> : BaseDal<TEntity, Guid>, ICourseService<TEntity> where TEntity : Course
    {
        public async Task<IDataResult<CourseDto?>> Activate(CourseDto dto)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var now = DateTimeOffset.Now;

                var course = await context.Course.FirstOrDefaultAsync(c => c.Id == dto.Id);
                if (course == null)
                    return new DataResult<CourseDto?>(ResultStatus.NotFound, ResponseMessages.NotFound, null);

                course.IsActive = true;
                course.DateModified = now;
                await context.SaveChangesAsync();
                return new DataResult<CourseDto?>(ResultStatus.Success, ResponseMessages.Success, dto);
            }
        }

        public async Task<IDataResult<CourseDto?>> Create(CourseDto dto)
        {
            var now = DateTimeOffset.Now;

            var course = new Course(Guid.NewGuid())
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                IsActive = true,
                DateCreated = now,
                DateModified = now
            };
            var result = await InsertAsync((TEntity)course) as Course;

            if (result != null)
                return new DataResult<CourseDto?>(ResultStatus.Success, ResponseMessages.Success, dto);

            return new DataResult<CourseDto?>(ResultStatus.Error, ResponseMessages.Error, null);
        }

        public async Task<IDataResult<CourseDto?>> Delete(CourseDto dto)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var now = DateTimeOffset.Now;

                var course = await context.Course.FirstOrDefaultAsync(c => c.Id == dto.Id);
                if (course == null)
                    return new DataResult<CourseDto?>(ResultStatus.NotFound, ResponseMessages.NotFound, null);

                course.IsActive = false;
                course.DateModified = now;
                await context.SaveChangesAsync();
                return new DataResult<CourseDto?>(ResultStatus.Success, ResponseMessages.Success, dto);
            }
        }

        public async Task<IDataResult<CourseData?>> GetAllCourses()
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var dataList = new List<CourseDto>();
                var query = (from c in context.Course.AsNoTracking()
                             join cat in context.Category.AsNoTracking() on c.CategoryId equals cat.Id
                             orderby c.DateModified descending
                             select new CourseDto
                             {
                                 Id = c.Id,
                                 CategoryId = c.CategoryId,
                                 Name = c.Name,
                                 Code = c.Code,
                                 Description = c.Description,
                                 CategoryName = cat.Name,
                                 IsActive = c.IsActive,
                                 DateCreated = c.DateCreated,
                                 DateModified = c.DateModified
                             });

                dataList = await query.ToListAsync();
                var data = new CourseData();
                data.data = dataList;
                data.numberOfAllCourses = dataList.Count;
                data.numberOfActiveCourses = dataList.Count(c => c.IsActive == true);
                data.numberOfPassiveCourses = dataList.Count(c => c.IsActive == false);

                if (query != null && dataList.Count > 0)
                    return new DataResult<CourseData?>(ResultStatus.Success, ResponseMessages.Success, data);

                return new DataResult<CourseData?>(ResultStatus.Error, ResponseMessages.Error, data);
            }
        }

        public async Task<IDataResult<CourseDto?>> GetCourse(Guid id)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var qData = await (from c in context.Course.AsNoTracking()
                                   join cat in context.Category.AsNoTracking() on c.CategoryId equals cat.Id
                                   where c.Id == id
                                   select new CourseDto
                                   {
                                       Id = c.Id,
                                       CategoryId = c.CategoryId,
                                       Name = c.Name,
                                       Code = c.Code,
                                       Description = c.Description,
                                       CategoryName = cat.Name,
                                       IsActive = c.IsActive,
                                       DateCreated = c.DateCreated,
                                       DateModified = c.DateModified
                                   }).FirstOrDefaultAsync();
                if (qData != null)
                {
                    return new DataResult<CourseDto?>(ResultStatus.Success, ResponseMessages.Success, qData);
                }
                else
                {
                    return new DataResult<CourseDto?>(ResultStatus.Error, ResponseMessages.Error, null);
                }
            }
        }

        public async Task<IDataResult<CourseDto?>> Update(CourseDto dto)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var now = DateTimeOffset.Now;
                var course = await context.Course.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (course != null)
                {
                    course.Name = dto.Name;
                    course.Code = dto.Code;
                    course.CategoryId = dto.CategoryId;
                    course.Description = dto.Description;
                    course.DateModified = now;
                    await context.SaveChangesAsync();
                    return new DataResult<CourseDto?>(ResultStatus.Success, ResponseMessages.Success, dto);
                }
                else
                {
                    return new DataResult<CourseDto?>(ResultStatus.Error, ResponseMessages.Error, null);
                }
            }

        }
    }
}

