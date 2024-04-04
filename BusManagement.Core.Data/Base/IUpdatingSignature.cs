namespace BusManagement.Core.Data.Base;

public interface IUpdatingSignature : IUpdatingTimeStamp
{
    Guid ModifiedBy { get; set; }
}
