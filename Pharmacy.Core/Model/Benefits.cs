namespace Pharmacy.Core.Model;

public class Benefits(string name, CoverageClass coverage, decimal oop, decimal cop)
{
    public string? Name { get; set; } = name;
    public CoverageClass Coverage { get; set; } = coverage;
    public decimal? OutOfPocket { get; set; } = oop;
    public decimal? Copay { get; set; } = cop;
}