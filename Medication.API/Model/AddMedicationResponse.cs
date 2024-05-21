namespace Medication.API.Model;

public record AddMedicationResponse(
	string Sciname,
	string Name,
	string Manufacturer,
	bool Generic);
