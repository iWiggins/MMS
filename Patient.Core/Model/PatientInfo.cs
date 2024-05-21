using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Core.Model;
public record PatientInfo(
	string FirstName,
	string LastName,
	DateOnly DateOfBirth,
	string SSN,
	string Insurance,
	string Address,
	string PhoneNumber);
