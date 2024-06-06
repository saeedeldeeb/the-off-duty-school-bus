namespace BusManagement.Core.DataModel.PaymentResources;

public record CreateSessionResource(
    string PaymentMethodTypes,
    long UnitAmount,
    string Currency,
    string ProductName,
    string Mode,
    string CustomerEmail,
    string SuccessUrl = "https://example.com/success",
    string CancelUrl = "https://example.com/cancel"
);
