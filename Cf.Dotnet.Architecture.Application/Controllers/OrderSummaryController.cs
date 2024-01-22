using System.Collections;
using Cf.Dotnet.Architecture.Application.Models;
using Cf.Dotnet.Architecture.Application.Queries;
using Cf.Dotnet.Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cf.Dotnet.Architecture.Application.Controllers;

public class OrderSummaryController : Controller
{
    private readonly IMediator _mediator;
    
    public OrderSummaryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Index()
    {
       var ordersSummaries = await this._mediator.Send(new GetOrdersSummaryQuery());
       return View(ordersSummaries);
    }

}
