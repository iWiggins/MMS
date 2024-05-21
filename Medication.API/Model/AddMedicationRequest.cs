namespace Medication.API.Model;

public class AddMedicationRequest
{
	public string? Sciname { get; set; }
	public string? Name { get; set; }
	public string? Manufacturer { get; set; }
	public bool Generic { get; set; }
}
