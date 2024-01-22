using System.Collections;
using Cf.Dotnet.Architecture.Application.Models;
using Cf.Dotnet.Database;
using Microsoft.AspNetCore.Mvc;

namespace Cf.Dotnet.Architecture.Application.Controllers;

public class OrderSummaryController : Controller
{
    private readonly DatabaseContext _context;
    
    public OrderSummaryController(DatabaseContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var ordersSummaries = new List<OrdersSummary>
        {
            new OrdersSummary(1, "Item", 10, "Miguel")
        };
        
        return View(ordersSummaries);
    }

}
