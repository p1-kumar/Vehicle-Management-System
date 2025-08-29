using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController(IVehicleService vehicleService) : Controller
    {
        private readonly IVehicleService _vehicleService = vehicleService;

        [HttpGet]
        public IActionResult GetVehicles()
        {
            return Ok(_vehicleService.GetAllVehicles());            
        }

        [HttpGet("{id}")]
        public IActionResult GetVehicleDetails(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
                return NotFound();
            // Logic to get vehicle details by id
            return Ok(vehicle);
        }

        [HttpPost]
        public IActionResult AddVehicle([FromBody] Vehicle vehicle) 
        {
            _vehicleService.AddVehicle(vehicle);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] Vehicle vehicle) 
        {
            // Logic to update vehicle details by id
            _vehicleService.UpdateVehicle(id, vehicle);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var result = _vehicleService.DeleteVehicle(id);
            if (!result)
                return NotFound();
            // Logic to delete vehicle by id
            return Ok();
        }
    }
}
