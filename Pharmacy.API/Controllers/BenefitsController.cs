using Microsoft.AspNetCore.Mvc;
using Pharmacy.API.Mappers;
using Pharmacy.API.Model;
using Pharmacy.Core;

namespace Pharmacy.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class BenefitsController(BenefitsMapper mapper, IPharmacyService service) : Controller
{
	private readonly BenefitsMapper _map = mapper;
	private readonly IPharmacyService _service = service;

	[HttpGet("coverage")]
	public async Task<ActionResult<PharmacyResponse>> getBenefits([FromQuery] PharmacyRequest request)
	{
		var requestor = _map.ToCore(request);
		var result = await _service.GetBenefitsAsync(requestor);
		var response = _map.ToApi(result);
		return response;
	}
}
