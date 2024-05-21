using Medication.Core.Model;

namespace Medication.API.Model;

public class ManufacturerMapper
{
	public ManufacturerInfo ToCore(AddManufacturerRequest request) =>
		new(request.Name);

	public string ToCore(GetManufacturerRequest request) => request.Name;

	public AddManufacturerResponse? ToApi(ManufacturerInfo? manufacturer) =>
		manufacturer is null ? null :
		new(manufacturer.Name);

	public GetManufacturersResponse ToApi(IEnumerable<ManufacturerInfo> manufacturers) => new(
		from manufacturer in manufacturers
		select new Manufacturer(manufacturer.Name));
}
