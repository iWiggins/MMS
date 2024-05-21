using Microsoft.EntityFrameworkCore;

namespace Provider.Core;
internal class ProviderContext : DbContext
{
	public DbSet<Model.DB.Provider> Provider { get; set; }
	public DbSet<Model.DB.Practice> Practice { get; set; }

	public string DbDirectory { get; }
	public string DbPath { get; }

	public ProviderContext()
	{
		var folder = Environment.SpecialFolder.LocalApplicationData;
		var path = Environment.GetFolderPath(folder);
		DbDirectory = Path.Join(path, "MMS");
		DbPath = Path.Join(DbDirectory, "provider.db");
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
