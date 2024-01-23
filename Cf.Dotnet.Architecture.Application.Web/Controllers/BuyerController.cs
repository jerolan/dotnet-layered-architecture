using AutoMapper;
using Cf.Dotnet.Architecture.Application.Models;
using Cf.Dotnet.Architecture.Domain.Entities;
using Cf.Dotnet.Architecture.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Cf.Dotnet.Architecture.Application.Controllers;

public class BuyerController : Controller
{
    private readonly IRepository<Buyer> buyerRepository;
    private readonly IMapper mapper;
    
    public BuyerController(IRepository<Buyer> buyerRepository, IMapper mapper)
    {
        this.mapper = mapper;
        this.buyerRepository = buyerRepository;
    }
    
    public async Task<ViewResult> Index()
    {
        var result = await this.buyerRepository.ToListAsync();
        var items = this.mapper.Map<List<BuyerModel>>(result);
        return View(items);
    }
}