using Medication.Core.Model;

namespace Medication.Core;

public enum DeletionResults
{
	OK,
	Failure,
	NotFound,
	InUse
}

public interface IMedicationDatabase
{
	Task<IEnumerable<MedicationInfo>> GetMedicationsAsync();
	Task<MedicationInfo?> GetMedicationByNameAsync(string name);
	Task<IEnumerable<MedicationInfo>> GetMedicationsByScientificNameAsync(string name);
	Task<IEnumerable<MedicationInfo>> GetMedicationsByManufacturerAsync(string name);
	Task<IEnumerable<MedicationInfo>> GetMedicationsWithSameChemistryAsync(string name);
	Task<MedicationInfo?> AddMedicationAsync(MedicationInfo medication);

	Task<IEnumerable<ManufacturerInfo>> GetManufacturersAsync();
	Task<ManufacturerInfo?> GetManufacturerAsync(string name);
	Task<ManufacturerInfo?> AddManufacturerAsync(ManufacturerInfo manufacturer);
	Task<DeletionResults> DeleteMedicationAsync(string name);
	Task<DeletionResults> DeleteManufacturerAsync(string name);
	Task<DeletionResults> DeleteScientificMedicationAsync(string sciname);
}
