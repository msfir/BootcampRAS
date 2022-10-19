using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bootcamp.MVC.NET.Models;
using Bootcamp.MVC.NET.Models.Entities;

namespace Bootcamp.MVC.NET.Controllers;

public class UserController : Controller
{
    private readonly AppDbContext _dbcontext;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, AppDbContext dbContext)
    {
        _dbcontext = dbContext;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<User> users = _dbcontext.Users.ToList();
        return View(users);
    }

    [HttpGet]
    public IActionResult DeleteData(User u) {
        User del = _dbcontext.Users.First(x => x.Id == u.Id);
        _dbcontext.Users.Remove(del);
        _dbcontext.SaveChanges();
        List<User> users = _dbcontext.Users.ToList();
        return View("Index", users);
    }

    [HttpGet]
    public IActionResult InputData() {
        return View();
    }

    [HttpPost]
    public IActionResult InputData(User u) {
        try {
            _dbcontext.Users.Add(u);
            _dbcontext.SaveChanges();
            List<User> users = _dbcontext.Users.ToList();
            return View("Index", users);
        } catch {
            return View();
        }
    }

    [HttpGet]
    public IActionResult EditData(int id) {
        User b = _dbcontext.Users.First(x => x.Id == id);
        return View(b);
    }

    [HttpPost]
    public IActionResult EditData(User u) {
        try {
            User edit = _dbcontext.Users.First(x => x.Id == u.Id);
            edit.Username = u.Username;
            edit.Password = u.Password;
            edit.Tipe = u.Tipe;
            _dbcontext.Users.Update(edit);

            // if (u.Tipe == "penjual") {
            //     _dbcontext.Penjuals.Add(new Penjual {
            //             
            //             })
            // }

            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        } catch {
            throw;
        }
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
