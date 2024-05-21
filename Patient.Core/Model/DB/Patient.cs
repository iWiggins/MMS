namespace Patient.Core.Model.DB;
internal class Patient
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateOnly DatOfBirth { get; set; }
	public string SSN { get; set; }
	public string Address;
	public string PhoneNumber;
}
