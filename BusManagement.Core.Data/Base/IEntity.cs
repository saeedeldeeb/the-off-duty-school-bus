namespace BusManagement.Core.Data.Base;

public interface IEntity<TPrimaryKey>
{
    TPrimaryKey Id { get; set; }
}
