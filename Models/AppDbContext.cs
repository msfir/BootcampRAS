using Microsoft.EntityFrameworkCore;
using Bootcamp.MVC.NET.Models.Entities;

namespace Bootcamp.MVC.NET.Models;

public class AppDbContext: DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Penjual> Penjuals { get; set; }
    public DbSet<Barang> Barangs { get; set; }
}
