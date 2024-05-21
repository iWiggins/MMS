using Pharmacy.API.Model;
using Pharmacy.Core.Model;
namespace Pharmacy.API.Mappers;

public class BenefitsMapper
{
    public Requestor ToCore(PharmacyRequest request) =>
        new(request.Id, request.Fname, request.Lname, request.Med, request.Dose);
    public PharmacyResponse ToApi(Benefits benefits) =>
        new(
            benefits.Coverage.ToString(),
            benefits.Copay ?? throw new MappingException(nameof(benefits.Copay), "null"),
            benefits.OutOfPocket ?? throw new MappingException(nameof(benefits.OutOfPocket), "null"));
}
