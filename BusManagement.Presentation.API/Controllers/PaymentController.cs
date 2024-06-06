using BusManagement.Core.DataModel.PaymentResources;
using BusManagement.Core.Services.PaymentServices;
using Microsoft.AspNetCore.Mvc;

namespace BusManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IStripeService _stripeService;

    public PaymentController(IStripeService stripeService)
    {
        _stripeService = stripeService;
    }

    [HttpPost("session")]
    public async Task<ActionResult<SessionResource>> CreateCheckoutSession(
        [FromBody] CreateSessionResource resource,
        CancellationToken cancellationToken
    )
    {
        var response = await _stripeService.CreateCheckoutSession(resource, cancellationToken);
        return Ok(response);
    }

    [HttpPost("customer")]
    public async Task<ActionResult<CustomerResource>> CreateCustomer(
        [FromBody] CreateCustomerResource resource,
        CancellationToken cancellationToken
    )
    {
        var response = await _stripeService.CreateCustomer(resource, cancellationToken);
        return Ok(response);
    }

    [HttpPost("charge")]
    public async Task<ActionResult<ChargeResource>> CreateCharge(
        [FromBody] CreateChargeResource resource,
        CancellationToken cancellationToken
    )
    {
        var response = await _stripeService.CreateCharge(resource, cancellationToken);
        return Ok(response);
    }
}
