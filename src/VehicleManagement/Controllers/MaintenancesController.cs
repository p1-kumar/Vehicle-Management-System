using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaintenancesController(IMaintenanceService maintenanceService) : Controller
    {       
        private readonly IMaintenanceService _maintenanceService = maintenanceService;

        [HttpGet]
        public IActionResult GetMaintenance(int vehicleId)
        {
            var maintenance = _maintenanceService.GetMaintenanceByVehicleId(vehicleId);
            if (maintenance == null)
                return NotFound();
            return Ok(maintenance);
        }

        [HttpPost]
        public IActionResult ScheduleMaintenance([FromBody] Maintenance maintenance)
        {
            var result = _maintenanceService.ScheduleMaintenance(maintenance.Id, maintenance.ScheduledDate);
            if (!result)
                return BadRequest();
            return Ok();
        }
    }
}
