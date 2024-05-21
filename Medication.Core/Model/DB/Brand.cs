using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Core.Model.DB;
public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MedId { get; set; }
    public int ManId { get; set; }
}
