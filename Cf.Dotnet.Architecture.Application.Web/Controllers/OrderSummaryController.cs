using Cf.Dotnet.Architecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cf.Dotnet.Architecture.Application.Controllers;

public class OrderSummaryController : Controller
{
    private readonly IMediator mediator;
    
    public OrderSummaryController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    public async Task<IActionResult> Index()
    {
       var ordersSummaries = await this.mediator.Send(new GetOrdersSummaryService());
       return View(ordersSummaries);
    }

    public async Task<IActionResult> OnPost(int id)
    {
        await this.mediator.Send(new ConfirmOrderService(id));
        return RedirectToPage("./Index");
    }
}
