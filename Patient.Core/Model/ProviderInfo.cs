using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Core.Model;
public record ProviderInfo(
	string Name,
	string Address,
	string PhoneNumber
	);
