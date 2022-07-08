## Asp.net core with EF

This is a guide to create, configure and execute an ASP.net core project with:

- entity framework
- migrations
- sqlite

### Create the project

From a terminal, execute:

> dotnet new mvc -o webmvc-ef_00

Then open the project from the terminal with:


> code webmvc-ef_00


### Configure the Development environment

1. Install the extensions :
  c#,nuget, sqlite

2. Then reload vscode.

3. Once the dependencies are automatically installed, go to the Launch tab and **create a launch.json file**,
select the .Net core option.

4. Launch the application and go to https://localhost:5001/, the website is up

### Add dependencies

1. Press Ctrl + Shift + p, then type 'nuget', select the option 'Add Package'
2. Add the package Microsoft.EntityFrameworkCore.Sqlite with latest version of release 6
3. Add the package Microsoft.EntityFrameworkCore.Design with latest version of release 6
4. Install the command line tools for Entity Framework (will simplify migrations creation), from the terminal:
> dotnet tool install --global dotnet-ef

### Configure database with your first entity
1. Create the file Models/Student.cs (this is our first entity) with the following content:
```
namespace webmvc_ef_00.Models {
    public class Student{
        public int Id {get; set;}
        public string Name {get; set;}
    }
}
```
2. Create the file Models/AppDbContext.cs (this is our configuration for the database connection with entity framework) with the following content:
```
using Microsoft.EntityFrameworkCore;

namespace webmvc_ef_00.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate(); // migrate the database from the startup
        }
        // with this, we are telling EntityFramework that Student should be treated as a database entity
        public DbSet<Student> Students { get; set; }
    }
}
```
3. In the file Program.cs, we need to configure the Database context as a service, so it can be used for the controllers:
```
...
using webmnv_ef_01.Models;
using Microsoft.EntityFrameworkCore;
...

 builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=mydatabase.sqlite"));


```

### Add your first migration
1. Let the entity-framework tools to identify and automatically create the migration with ('InitializeDatabase' is the name of the migration, you can use a different name):
```
dotnet ef migrations add InitializeDatabase
```
2. Your new migration has been added to the Migrations folder


### Let's create some data
1. In the file Controllers/HomeController.cs, add the AppDbContext as a service
```
private readonly AppDbContext _dbContext;

public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
{
  _logger = logger;
  _dbContext = dbContext;
}
```
2. In the Index method, add the following code to create and list students
```
public IActionResult Index()
{
  // add some students if none is found
  if (_dbContext.Students.Count() == 0) {
    _dbContext.Add(new Student{Name = "Mirtha Jimenez"});
    _dbContext.Add(new Student{Name = "Roxana Jimenez"});
    _dbContext.Add(new Student{Name = "Reina Jimenez"});
    _dbContext.SaveChanges();
  }            
  // print them in the log
  foreach (var item in _dbContext.Students)
  {
    _logger.LogInformation($"Student :id {item.Id} :name '{item.Name}'");
  }
  return View();
}
```
3. Open the database file with a DB-browser, and you will be able to see the created data
