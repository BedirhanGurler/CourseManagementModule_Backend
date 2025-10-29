using Adaptteen.DataAccess.BaseEntity;

namespace Adaptteen.DataAccess.Model
{
    public class Category(Guid id) : BaseEntity<Guid>(id)
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
