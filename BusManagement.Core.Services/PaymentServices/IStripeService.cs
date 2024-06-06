using BusManagement.Core.DataModel.PaymentResources;

namespace BusManagement.Core.Services.PaymentServices;

public interface IStripeService
{
    Task<SessionResource> CreateCheckoutSession(
        CreateSessionResource resource,
        CancellationToken cancellationToken
    );
    Task<CustomerResource> CreateCustomer(
        CreateCustomerResource resource,
        CancellationToken cancellationToken
    );
    Task<ChargeResource> CreateCharge(
        CreateChargeResource resource,
        CancellationToken cancellationToken
    );
}
