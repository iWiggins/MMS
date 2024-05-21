using Medication.API.Model;
using Medication.Core;
using Microsoft.AspNetCore.Mvc;

namespace Medication.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class MedicationController(IMedicationDatabase db, MedicationMapper map) : ControllerBase
{
	[HttpPost]
    public async Task<ActionResult<AddMedicationResponse?>> AddMedicationAsync([FromBody] AddMedicationRequest request)
    {
        var medication = map.ToCore(request);
        var result = await db.AddMedicationAsync(medication);
        var response = map.ToApi(result);
        return response is null ? BadRequest() : Accepted(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetMedicationResponse>> GetMedicationAsync([FromQuery] GetMedicationRequest request)
    {
        if(request.Name is not null)
        {
            var medication = await db.GetMedicationByNameAsync(request.Name);
            return medication is not null ? Ok(map.ToApi([medication])) : NotFound();

		}
        else if(request.SciName is not null)
        {
            var medications = await db.GetMedicationsByScientificNameAsync(request.SciName);
            return medications.Any() ? Ok(map.ToApi(medications)) : NotFound();
        }
        else
        {
            var medications = await db.GetMedicationsAsync();
            return medications.Any() ? Ok(map.ToApi(medications)) : NotFound();
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMedicationAsync([FromQuery] DeleteMedicationRequest request)
    {
        if(request.Name is not null)
        {
            return await db.DeleteMedicationAsync(request.Name) switch
            { 
                DeletionResults.OK => Ok(),
                DeletionResults.NotFound => NotFound(),
                _ => StatusCode(500)
            };
        }
        else if(request.SciName is not null)
        {
            return await db.DeleteScientificMedicationAsync(request.SciName) switch
            {
                DeletionResults.OK => Ok(),
                DeletionResults.NotFound => NotFound(),
                DeletionResults.InUse => Conflict(),
                DeletionResults.Failure => StatusCode(500),
                _ => StatusCode(500)
            };
        }
        else
        {
            return BadRequest();
        }
    }
}
