using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adaptteen.Common.Enums;

namespace Adaptteen.Common.Results
{
    public interface IGenericResult
    {
        string? Message { get; set; }
        string? Description { get; set; }
        string? Status { get; }
        //bool Result { get; set; }
        ResultStatus ResultStatus { get; set; }
    }
}
