namespace BusManagement.Core.DataModel.PaymentResources;

public record CreateCardResource(
    string Name,
    string Number,
    string ExpiryYear,
    string ExpiryMonth,
    string Cvc
);
