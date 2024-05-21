using Medication.API.Model;
using Medication.Core;
using Microsoft.AspNetCore.Mvc;

namespace Medication.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class ManufacturerController(IMedicationDatabase db, ManufacturerMapper map) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<AddManufacturerResponse>> AddManufacturerAsync([FromBody] AddManufacturerRequest request)
	{
		var manufacturer = map.ToCore(request);
		var result = await db.AddManufacturerAsync(manufacturer);
		var response = map.ToApi(result);
		return response is null ? BadRequest() : Accepted(response);
	}

	[HttpGet]
	public async Task<ActionResult<GetManufacturersResponse>> GetManufacturerAsync([FromQuery] GetManufacturerRequest request)
	{
		if(request.Name is null)
		{
			var result = await db.GetManufacturersAsync();
			var response = map.ToApi(result);
			return Ok(response);
		}
		else
		{
			var manufacturer = map.ToCore(request);
			var result = await db.GetManufacturerAsync(manufacturer);
			var response = map.ToApi(result);
			return response is null ? NotFound() : Ok(response);
		}
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteManufacturerAsync([FromQuery] DeleteManufacturerRequest request)
	{
		return await db.DeleteManufacturerAsync(request.Name) switch
		{
			DeletionResults.OK => Ok(),
			DeletionResults.Failure => StatusCode(500),
			DeletionResults.NotFound => NotFound(),
			DeletionResults.InUse => Conflict(),
			_ => StatusCode(500)
		};
	}
}
