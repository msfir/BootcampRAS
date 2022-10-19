using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bootcamp.MVC.NET.Models;
using Bootcamp.MVC.NET.Models.Entities;

namespace Bootcamp.MVC.NET.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _dbcontext;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, AppDbContext dbContext)
    {
        _dbcontext = dbContext;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Barang> barangs = _dbcontext.Barangs.ToList();
        return View(barangs);
    }

    [HttpGet]
    public IActionResult DeleteData(Barang b) {
        Barang del = _dbcontext.Barangs.First(x => x.Id == b.Id);
        _dbcontext.Barangs.Remove(del);
        _dbcontext.SaveChanges();
        List<Barang> barangs = _dbcontext.Barangs.ToList();
        return View("Index", barangs);
    }

    [HttpGet]
    public IActionResult InputData() {
        return View();
    }

    [HttpPost]
    public IActionResult InputData(RequestBarang b) {
        var upFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(upFolder)) {
            Directory.CreateDirectory(upFolder);
        }
        var filename = $"{b.Kode}-{b.Image.FileName}";
        var filepath = Path.Combine(upFolder, filename);
        using var stream = System.IO.File.Create(filepath);
        if (b.Image != null) {
            b.Image.CopyTo(stream);
        }
        var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/images/{filename}";
        _dbcontext.Barangs.Add(new Barang {
                IdPenjual = 1,
                Kode = b.Kode,
                Nama = b.Nama,
                Harga = b.Harga,
                Stok = b.Stok,
                Description = b.Description,
                FileName = filename,
                Url = url
                });
        _dbcontext.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditData(int id) {
        Barang b = _dbcontext.Barangs.First(x => x.Id == id);
        return View(b);
    }

    [HttpPost]
    public IActionResult EditData(Barang b) {
        Barang edit = _dbcontext.Barangs.First(x => x.Id == b.Id);
        edit.Kode = b.Kode;
        edit.Nama = b.Nama;
        edit.Harga = b.Harga;
        edit.Stok = b.Stok;
        edit.Description = b.Description;
        _dbcontext.Barangs.Update(edit);
        _dbcontext.SaveChanges();
        return RedirectToAction("Index");
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
