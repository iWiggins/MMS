namespace Pharmacy.API.Model;

public record PharmacyRequest(
    string Id,
    string Fname,
    string Lname,
    string Med,
    int Dose
);
