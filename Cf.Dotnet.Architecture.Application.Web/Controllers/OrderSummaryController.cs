using Cf.Dotnet.Architecture.Application.Commands;
using Cf.Dotnet.Architecture.Application.Queries;
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
       var ordersSummaries = await this.mediator.Send(new GetOrdersSummaryQuery());
       return View(ordersSummaries);
    }

    public async Task<IActionResult> OnPost(int id)
    {
        await this.mediator.Send(new ReplaceOrderCommand(id));
        return RedirectToPage("./Index");
    }
}
