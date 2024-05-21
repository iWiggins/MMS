using Pharmacy.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core;
public interface IPharmacyService
{
    Benefits GetBenefits(Requestor request);
    Task<Benefits> GetBenefitsAsync(Requestor request);
}
