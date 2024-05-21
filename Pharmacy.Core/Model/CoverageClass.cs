using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.Model;
public enum CoverageClass
{
    [Description("Generic")]
    A = 1,
    [Description("Preferred")]
    B = 2,
    [Description("Specialty")]
    C = 3
}
