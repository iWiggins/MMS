namespace Provider.Core.Model.DB;
internal class Provider
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Address { get; set; }
	public string PhoneNumber { get; set; }

	// EF auto-generated join table
	public ICollection<Practice> Practices { get; set; }
}
