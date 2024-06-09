using System.Security.Claims;
using BusManagement.Core.Common.Enums;
using BusManagement.Core.DataModel.PaymentResources;
using BusManagement.Core.Services;
using BusManagement.Core.Services.PaymentServices;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BusManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IStripeService _stripeService;
    private readonly IRentService _rentService;

    public PaymentController(IStripeService stripeService, IRentService rentService)
    {
        _stripeService = stripeService;
        _rentService = rentService;
    }

    [HttpPost("session")]
    public async Task<ActionResult<SessionResource>> CreateCheckoutSession(
        Guid rentId,
        CancellationToken cancellationToken
    )
    {
        var resource = new CreateSessionResource(
            PaymentMethodTypes: "card",
            UnitAmount: 100,
            Currency: "usd",
            ProductName: "Bus Rent",
            Mode: "payment",
            CustomerEmail: User.FindFirstValue(ClaimTypes.Email) ?? "",
            Metadata: new Dictionary<string, string> { { "rentId", rentId.ToString() } },
            SuccessUrl: "https://example.com/success",
            CancelUrl: "https://example.com/cancel"
        );
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

    [HttpPost("webhook")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Index()
    {
        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret =
            "whsec_82f52c02970bd22d31421d1acacb75277781efa44aad4370bcde16a1f5dbb932";

        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                endpointSecret,
                throwOnApiVersionMismatch: false
            );
            // Handle the event
            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                await _rentService.UpdateRentStatusAsync(
                    Guid.Parse(
                        paymentIntent?.Metadata.GetValueOrDefault("rentId") ?? Guid.Empty.ToString()
                    ),
                    RentStatusEnum.Succeeded
                );
            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            Console.WriteLine("Failed to handle event: {0}", e.Message);
            return BadRequest();
        }
    }
}
