using Medication.Core.Model;

namespace Medication.API.Model;

public class MedicationMapper
{
	public MedicationInfo ToCore(AddMedicationRequest request) =>
		new(request.Sciname, request.Name, request.Manufacturer, request.Generic);

	public AddMedicationResponse? ToApi(MedicationInfo? medication) =>
		medication is null ? null :
		new(medication.ScientificName, medication.Name, medication.Manufacturer, medication.IsGeneric);

	public GetMedicationResponse ToApi(IEnumerable<MedicationInfo> medications) => new(
		from medication in medications
		select new Medication(medication.ScientificName, medication.Name, medication.Manufacturer, medication.IsGeneric));
}
