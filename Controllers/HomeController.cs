using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webmnv_ef_01.Models;

namespace webmnv_ef_01.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {   // add some students if none is found
        if (_dbContext.Students.Count() == 0)
        {
            _dbContext.Add(new Student { Name = "Mirtha Jimenez" });
            _dbContext.Add(new Student { Name = "Roxana Jimenez" });
            _dbContext.Add(new Student { Name = "Reina Jimenez" });
            _dbContext.SaveChanges();
        }
        // print them in the log
        foreach (var item in _dbContext.Students)
        {
            _logger.LogInformation($"Student :id {item.Id} :name '{item.Name}'");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
