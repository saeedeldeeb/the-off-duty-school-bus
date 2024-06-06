namespace BusManagement.Core.DataModel.PaymentResources;

public record CreateCustomerResource(string Email, string Name, CreateCardResource Card);
