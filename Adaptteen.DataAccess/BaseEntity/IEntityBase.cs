namespace Adaptteen.DataAccess.BaseEntity
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; }
    }
}
