namespace Pharmacy.API.Model;

public record PharmacyResponse(
    string CoverageClass,
    decimal Copay,
    decimal OutOfPocket
);