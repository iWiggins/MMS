using Microsoft.EntityFrameworkCore;

namespace Patient.Core;
internal class PatientContext : DbContext
{
	public DbSet<Model.DB.Prescription> Prescription { get; set; }
	public DbSet<Model.DB.Patient> Patient { get; set; }

	public string DbDirectory { get; }
	public string DbPath { get; }

	public PatientContext()
	{
		var folder = Environment.SpecialFolder.LocalApplicationData;
		var path = Environment.GetFolderPath(folder);
		DbDirectory = Path.Join(path, "MMS");
		DbPath = Path.Join(DbDirectory, "patient.db");
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
