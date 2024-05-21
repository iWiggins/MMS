using Medication.Core.Model.DB;
using Microsoft.EntityFrameworkCore;

namespace Medication.Core;

public class MedicationContext : DbContext
{
    public DbSet<Model.DB.Medication> Medication { get; set; }
    public DbSet<Brand> Brand { get; set; }
    public DbSet<Generic> Generic { get; set; }
    public DbSet<Manufacturer> Manufacturer { get; set; }

    public string DbDirectory { get; }
    public string DbPath { get; }

    public MedicationContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbDirectory = Path.Join(path, "MMS");
        DbPath = Path.Join(DbDirectory, "medication.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if(!Directory.Exists(DbDirectory))
        {
            Directory.CreateDirectory(DbDirectory);
        }
    }
}
