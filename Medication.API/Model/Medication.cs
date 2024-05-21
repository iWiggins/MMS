namespace Medication.API.Model;

public record Medication(
	string ScientificName,
	string Name,
	string Manufacturer,
	bool IsGeneric);
