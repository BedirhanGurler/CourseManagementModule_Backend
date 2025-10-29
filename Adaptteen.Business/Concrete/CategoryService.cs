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
    public class CategoryService<TEntity> : BaseDal<TEntity, Guid>, ICategoryService<TEntity> where TEntity : Category
    {
        public async Task<IDataResult<CategoryDto?>> Create(CategoryDto dto)
        {
            var now = DateTimeOffset.Now;

            var category = new Category(Guid.NewGuid())
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                IsActive = true,
                DateCreated = now,
                DateModified = now
            };
            var result = await InsertAsync((TEntity)category) as Category;

            if (result != null)
                return new DataResult<CategoryDto?>(ResultStatus.Success, ResponseMessages.Success, dto);

            return new DataResult<CategoryDto?>(ResultStatus.Error, ResponseMessages.Error, null);
        }

        public async Task<IDataResult<CategoryDto?>> Delete(CategoryDto dto)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var now = DateTimeOffset.Now;

                var course = await context.Category.FirstOrDefaultAsync(c => c.Id == dto.Id);
                if (course == null)
                    return new DataResult<CategoryDto?>(ResultStatus.NotFound, ResponseMessages.NotFound, null);

                course.IsActive = false;
                course.DateModified = now;
                await context.SaveChangesAsync();
                return new DataResult<CategoryDto?>(ResultStatus.Success, ResponseMessages.Success, dto);
            }
        }

        public async Task<IDataResult<CategoryData?>> GetAllCategories()
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var dataList = new List<CategoryDto>();
                var query = (from c in context.Category.AsNoTracking()
                             orderby c.DateModified descending
                             select new CategoryDto
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Code = c.Code,
                                 Description = c.Description,
                                 IsActive = c.IsActive,
                                 DateCreated = c.DateCreated,
                                 DateModified = c.DateModified
                             });

                dataList = await query.ToListAsync();
                var data = new CategoryData();
                data.data = dataList;
                data.numberOfAllCategories = dataList.Count;
                data.numberOfActiveCategories = dataList.Count(c => c.IsActive == true);
                data.numberOfPassiveCategories = dataList.Count(c => c.IsActive == false);

                if (query != null && dataList.Count > 0)
                    return new DataResult<CategoryData?>(ResultStatus.Success, ResponseMessages.Success, data);

                return new DataResult<CategoryData?>(ResultStatus.Error, ResponseMessages.Error, data);
            }
        }

        public async Task<IDataResult<CategoryDto?>> GetCategory(Guid id)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var qData = await (from c in context.Category.AsNoTracking()
                                   where c.Id == id
                                   select new CategoryDto
                                   {
                                       Id = c.Id,
                                       Name = c.Name,
                                       Code = c.Code,
                                       Description = c.Description,
                                       IsActive = c.IsActive,
                                       DateCreated = c.DateCreated,
                                       DateModified = c.DateModified
                                   }).FirstOrDefaultAsync();
                if (qData != null)
                {
                    return new DataResult<CategoryDto?>(ResultStatus.Success, ResponseMessages.Success, qData);
                }
                else
                {
                    return new DataResult<CategoryDto?>(ResultStatus.Error, ResponseMessages.Error, null);
                }
            }
        }

        public async Task<IDataResult<CategoryDto?>> Update(CategoryDto dto)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var now = DateTimeOffset.Now;
                var course = await context.Course.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (course != null)
                {
                    course.Name = dto.Name;
                    course.Code = dto.Code;
                    course.Description = dto.Description;
                    course.DateModified = now;
                    await context.SaveChangesAsync();
                    return new DataResult<CategoryDto?>(ResultStatus.Success, ResponseMessages.Success, dto);
                }
                else
                {
                    return new DataResult<CategoryDto?>(ResultStatus.Error, ResponseMessages.Error, null);
                }
            }
        }
    }
}
