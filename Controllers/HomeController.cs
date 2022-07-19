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
    public IActionResult ListLocation()
    {

        return View(_dbContext.Locations.ToList());

    }
    public IActionResult CreateLocation()
    {

        return View(_dbContext.Locations.ToList());

    }

    [HttpPost]
    public IActionResult DeleteLocation(int Id)
    {
        _logger.LogInformation($"Location SELECTED :id {Id}");
        Location location = _dbContext.Locations.Where(s => s.Id == Id).First();
        _dbContext.Locations.Remove(location);
        _dbContext.SaveChanges();
        return RedirectToAction("ListLocation", "Home");
    }

    public ActionResult Edit(int id)
    {
        return View(_dbContext.Locations.Where(s => s.Id == id).First());
    }

    [HttpPost]
    public ActionResult SaveLocation(Location input)
    {
        _logger.LogInformation($"received data {input}");
        var location = _dbContext.Locations.Where(s => s.Id == input.Id).First();
        location.Name = input.Name;
        _dbContext.SaveChanges();
        return RedirectToAction("ListLocation", "Home");
    }



    public IActionResult Index()
    {
        ListOrCreateStudents();
        ListOrCreateContacts();
        ListOrCreateLocationsAndCustomers();
        return View();
    }

    private void ListOrCreateLocationsAndCustomers()
    {
        if (_dbContext.Locations.Count() == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                _dbContext.Add(new Location { Name = $"LocationNumber {i}" });
            }
            _dbContext.SaveChanges();
        }
        var location = getOrCreateLocation("km 9, Urbanizacion Rosedal");

        if (_dbContext.Customers.Count() == 0)
        {
            for (int i = 0; i < 20; i++)
            {
                _dbContext.Add(entity: new Customer { Name = $"NameCustomer {i}", Location = location });
            }
            _dbContext.SaveChanges();
        }
    }

    private void ListOrCreateContacts()
    {
        if (_dbContext.Contacts.Count() == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                _dbContext.Add(new Contact { Name = $"Nombre {i}", Address = $"Barrio {i}" });
            }
            _dbContext.SaveChanges();
        }
    }

    private void ListOrCreateStudents()
    {
        // add some students if none is found
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
    }

    public Location getOrCreateLocation(string name)
    {
        var location = _dbContext.Locations.Where(l => l.Name.Equals(name)).SingleOrDefault();
        if (location == null)
        {
            location = new Location { Name = name };
            _dbContext.Add(location);
            _dbContext.SaveChanges();
        }
        return location;
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
