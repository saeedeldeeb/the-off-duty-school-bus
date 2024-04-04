namespace BusManagement.Core.Data.Base;

public interface ICreatingSignature : ICreatingTimeStamp
{
    Guid CreatedBy { get; set; }
}
