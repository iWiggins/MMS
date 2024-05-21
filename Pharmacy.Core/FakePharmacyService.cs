using Pharmacy.Core.Model;

namespace Pharmacy.Core;
public class FakePharmacyService : IPharmacyService
{
    private readonly Random _rand = new();

    public Benefits GetBenefits(string id, string fname, string lname, string medication, int dose)
    {
        string name = medication;
        CoverageClass coverage = (CoverageClass)_rand.Next(1, 4);
        decimal oop = _rand.Next(1000, 10000) / 100m;
        decimal copay = 35m;
        return new(name, coverage, oop, copay);
    }

	public Benefits GetBenefits(Requestor request)
    {
		string name = request.Medication;
		CoverageClass coverage = (CoverageClass)_rand.Next(1, 4);
		decimal oop = _rand.Next(1000, 10000) / 100m;
		decimal copay = 35m;
		return new(name, coverage, oop, copay);
	}

	public async Task<Benefits> GetBenefitsAsync(string id, string fname, string lname, string medication, int dose)
    {
        return await Task.Run(() => GetBenefits(id, fname, lname, medication, dose));
    }

	public async Task<Benefits> GetBenefitsAsync(Requestor request)
    {
        return await Task.Run(() => GetBenefits(request));
    }
}
