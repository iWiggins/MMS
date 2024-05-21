using Medication.Core.Model;
using Medication.Core.Model.DB;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Medication.Core;
public class MedicationDatabase(MedicationContext db) : IMedicationDatabase
{
	public async Task<ManufacturerInfo?> AddManufacturerAsync(ManufacturerInfo manufacturer)
	{
		Manufacturer man = new()
		{
			Name = manufacturer.Name
		};
		var manTracker = db.Add(man);
		if(await db.SaveChangesAsync() < 1) return null;
		return new(manTracker.Entity.Name);		
	}
	public async Task<MedicationInfo?> AddMedicationAsync(MedicationInfo medication)
	{
		// Check if manufacturer exists
		var manIds = from man in db.Manufacturer where medication.Manufacturer == man.Name select man.Id;

		int manId;

		try { manId = await manIds.FirstAsync(); } catch(InvalidOperationException) { return null; }

		// check if scientific name exists
		var sciIds = from med in db.Medication where medication.Name == medication.ScientificName select med.Id;

		int scientificId;

		try { scientificId = await sciIds.FirstAsync();  } 
		catch(InvalidOperationException)
		{
			// Create scientific entry if not found
			Model.DB.Medication newMed = new()
			{
				Name = medication.ScientificName
			};

			var medTracker = await db.AddAsync(newMed);
			if(await db.SaveChangesAsync() < 1) return null;

			scientificId = medTracker.Entity.Id;
		}
		
		if(medication.IsGeneric)
		{
			Model.DB.Generic gen = new()
			{
				Name = medication.Name,
				ManId = manId,
				MedId = scientificId
			};

			var medTracker = await db.AddAsync(gen);
			if(await db.SaveChangesAsync() < 1) return null;
			return new(medTracker.Entity.Name, medication.ScientificName, medication.Manufacturer, true);
		}
		else
		{
			Model.DB.Brand brn = new()
			{
				Name = medication.Name,
				ManId = manId,
				MedId = scientificId
			};

			var medTracker = await db.AddAsync(brn);
			if(await db.SaveChangesAsync() < 1) return null;
			return new(medTracker.Entity.Name, medication.ScientificName, medication.Manufacturer, false);
		}
	}
	public async Task<IEnumerable<ManufacturerInfo>> GetManufacturersAsync()
	{
		var manufacturers = from man in db.Manufacturer select new ManufacturerInfo(man.Name);
		return await manufacturers.ToListAsync();
	}
	public async Task<ManufacturerInfo?> GetManufacturerAsync(string name)
	{
		var manufacturers = from man in db.Manufacturer where man.Name == name select new ManufacturerInfo(man.Name);
		
		try { return await manufacturers.FirstAsync(); }catch(InvalidOperationException) { return null; }
	}
	public async Task<MedicationInfo?> GetMedicationByNameAsync(string name)
	{
		var generics =
			from generic in db.Generic
			where generic.Name == name
			join medication in db.Medication
			on generic.MedId equals medication.Id
			join manufacturer in db.Manufacturer
			on generic.ManId equals manufacturer.Id
			select new MedicationInfo(medication.Name, generic.Name, manufacturer.Name, true);

		var brandname =
			from brand in db.Brand
			where brand.Name == name
			join medication in db.Medication
			on brand.MedId equals medication.Id
			join manufacturer in db.Manufacturer
			on brand.ManId equals manufacturer.Id
			select new MedicationInfo(medication.Name, brand.Name, manufacturer.Name, false);

		try { return await generics.Concat(brandname).FirstAsync(); } catch(InvalidOperationException) { return null; }
	}
	public async Task<IEnumerable<MedicationInfo>> GetMedicationsAsync()
	{
		var generics =
			from generic in db.Generic
			join medication in db.Medication
			on generic.MedId equals medication.Id
			join manufacturer in db.Manufacturer
			on generic.ManId equals manufacturer.Id
			select new MedicationInfo(medication.Name, generic.Name, manufacturer.Name, true);

		var brandname =
			from brand in db.Brand
			join medication in db.Medication
			on brand.MedId equals medication.Id
			join manufacturer in db.Manufacturer
			on brand.ManId equals manufacturer.Id
			select new MedicationInfo(medication.Name, brand.Name, manufacturer.Name, false);

		return (await generics.ToListAsync()).Concat(await brandname.ToListAsync());
	}
	public async Task<IEnumerable<MedicationInfo>> GetMedicationsByScientificNameAsync(string name)
	{
		var generics =
			from medication in db.Medication
			where medication.Name == name
			join generic in db.Generic
			on medication.Id equals generic.MedId
			join manufacturer in db.Manufacturer
			on generic.ManId equals manufacturer.Id
			select new MedicationInfo(medication.Name, generic.Name, manufacturer.Name, true);

		var brandname =
			from medication in db.Medication
			where medication.Name == name
			join brand in db.Brand
			on medication.Id equals brand.MedId
			join manufacturer in db.Manufacturer
			on brand.ManId equals manufacturer.Id
			select new MedicationInfo(medication.Name, brand.Name, manufacturer.Name, false);

		return (await generics.ToListAsync()).Concat(await brandname.ToListAsync());
	}
	public async Task<IEnumerable<MedicationInfo>> GetMedicationsWithSameChemistryAsync(string name)
	{
		var medication = await GetMedicationByNameAsync(name);

		if(medication is null) return [];

		else return await GetMedicationsByScientificNameAsync(medication.ScientificName);
	}
	public async Task<IEnumerable<MedicationInfo>> GetMedicationsByManufacturerAsync(string name)
	{
		var generics =
		from med in db.Medication
		join gen in db.Generic
		on med.Id equals gen.MedId
		join man in db.Manufacturer
		on gen.ManId equals man.Id
		where man.Name == name
		select new MedicationInfo(med.Name, gen.Name, man.Name, true);

		var branded =
		from med in db.Medication
		join brand in db.Brand
		on med.Id equals brand.MedId
		join man in db.Manufacturer
		on brand.ManId equals man.Id
		where man.Name == name
		select new MedicationInfo(med.Name, brand.Name, man.Name, false);

		var meds = (await generics.ToListAsync()).Concat(await branded.ToListAsync());

		return meds;
	}
	public async Task<DeletionResults> DeleteMedicationAsync(string name)
	{
		var query =
		from med in db.Medication
		where med.Name == name
		select med;
		Model.DB.Medication medication;
		try { medication = await query.FirstAsync(); } catch(InvalidOperationException) { return DeletionResults.NotFound; }
		db.Remove(medication);
		return await db.SaveChangesAsync() > 0 ? DeletionResults.OK : DeletionResults.Failure;
	}
	public async Task<DeletionResults> DeleteManufacturerAsync(string name)
	{
		var mans =
		from man in db.Manufacturer
		where man.Name == name
		select man;
		Model.DB.Manufacturer manufacturer;
		try { manufacturer = await mans.FirstAsync(); } catch(InvalidOperationException) { return DeletionResults.NotFound; }

		// check if manufacturer in use
		var meds = await GetMedicationsByManufacturerAsync(name);
		if(meds.Any()) return DeletionResults.InUse;

		db.Remove(manufacturer);
		return await db.SaveChangesAsync() > 0 ? DeletionResults.OK : DeletionResults.Failure;
	}

	public async Task<DeletionResults> DeleteScientificMedicationAsync(string sciname)
	{
		var meds =
		from med in db.Medication
		where med.Name == sciname
		select med;

		Model.DB.Medication scimed;
		try { scimed = await meds.FirstAsync(); }catch(InvalidOperationException) { return DeletionResults.NotFound; }

		var generics =
		from med in db.Medication
		join gen in db.Generic
		on med.Id equals gen.MedId
		join man in db.Manufacturer
		on gen.ManId equals man.Id
		where med.Name == sciname
		select 0;

		if(await generics.AnyAsync()) return DeletionResults.InUse;

		var branded =
		from med in db.Medication
		join brand in db.Brand
		on med.Id equals brand.MedId
		join man in db.Manufacturer
		on brand.ManId equals man.Id
		where med.Name == sciname
		select 0;

		if(await branded.AnyAsync()) return DeletionResults.InUse;

		db.Remove(scimed);
		return await db.SaveChangesAsync() > 0 ? DeletionResults.OK : DeletionResults.Failure;
	}
}
