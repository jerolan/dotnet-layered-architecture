using AutoMapper;
using Cf.Dotnet.Architecture.Application.Models;
using Cf.Dotnet.Database;
using Microsoft.AspNetCore.Mvc;

namespace Cf.Dotnet.Architecture.Application.Controllers;

public class BuyerController : Controller
{
    private readonly DatabaseContext context;
    private readonly IMapper mapper;
    
    public BuyerController(DatabaseContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }
    
    public IActionResult Index()
    {
        var result = this.context.Buyers.ToList();
        var items = this.mapper.Map<List<BuyerModel>>(result);
        return View(items);
    }
}