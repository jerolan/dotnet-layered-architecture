using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cf.Dotnet.Architecture.Application.Models;

namespace Cf.Dotnet.Architecture.Application.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}