namespace BusManagement.Core.DataModel.PaymentResources;

public record CreateSessionResource(
    string PaymentMethodTypes,
    long UnitAmount,
    string Currency,
    string ProductName,
    string Mode,
    string CustomerEmail,
    Dictionary<string, string>? Metadata = null,
    string SuccessUrl = "https://example.com/success",
    string CancelUrl = "https://example.com/cancel"
);
