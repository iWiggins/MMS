namespace Patient.Core.Model.DB;
internal class Prescription
{
	public int Id { get; set; }
	public int PatientId { get; set; }
	public int ProviderId { get; set; }
	public string MedicationName {  get; set; }
	public int Dosage { get; set; }
	public DateOnly PrescribedOn { get; set; }
	public int RefillCount { get; set; }
}
