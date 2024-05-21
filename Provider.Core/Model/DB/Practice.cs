using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Core.Model.DB;
internal class Practice
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	public string PhoneNumber { get; set; }
	public PracticeLevel Level { get; set; }

	// EF auto-generated join table
	public ICollection<Provider> Providers { get; set; }
}
