using Adaptteen.Common.DTOs;
using Adaptteen.Common.Results;
using Adaptteen.DataAccess.Abstract;
using Adaptteen.DataAccess.Model;

namespace Adaptteen.Business.Abstract
{
    public interface ICategoryService<TEntity> : IBaseDal<TEntity, Guid> where TEntity : Category
    {
        Task<IDataResult<CategoryDto?>> Create(CategoryDto dto);
        Task<IDataResult<CategoryDto?>> Update(CategoryDto dto);
        Task<IDataResult<CategoryDto?>> Delete(CategoryDto dto);
        Task<IDataResult<CategoryData?>> GetAllCategories();
        Task<IDataResult<CategoryDto?>> GetCategory(Guid id);
    }
}
