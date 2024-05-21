using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.Model;
public record Requestor(
    string Id,
    string Fname,
    string Lname,
    string Medication,
    int Dose);
