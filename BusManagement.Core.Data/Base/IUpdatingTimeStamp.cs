namespace BusManagement.Core.Data.Base;

public interface IUpdatingTimeStamp
{
    DateTime? ModificationDateTime { get; set; }
}
