using System.ComponentModel.DataAnnotations;

namespace Bootcamp.MVC.NET.Models;

public class RequestBarang {
    [StringLength(10)]
    public string Kode {get;set;}
    [StringLength(100)]
    public string Nama {get ;set;}
    public string Description {get;set;}
    public decimal Harga {get;set;}
    public int Stok { get; set; }

    public IFormFile? Image { get; set; }

    public string FileName {get; set;}
    public string Url {get; set;}
}
