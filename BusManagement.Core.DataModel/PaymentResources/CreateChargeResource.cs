namespace BusManagement.Core.DataModel.PaymentResources;

public record CreateChargeResource(
    string Currency,
    long Amount,
    string CustomerId,
    string ReceiptEmail,
    string Description
);
