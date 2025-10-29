using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Adaptteen.Common.Validations.Abstract
{
    public interface IModelStateResponseService
    {
        string HandleErrorMessage(ModelStateDictionary modelState);
    }
}
