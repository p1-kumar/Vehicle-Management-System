using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;
using VehicleManagement.Services;

namespace VehicleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripsController(ITripService tripService) : Controller
    {
        private readonly ITripService _tripService = tripService;

        [HttpGet]
        public IActionResult GetTrips(int vehicleId)
        {
            return Ok(_tripService.GetTripsByVehicleId(vehicleId));            
        }

        [HttpGet("{id}")]
        public IActionResult GetTripDetails(int id)
        {
            var trip = _tripService.GetTrip(id);
            if (trip == null)
                return NotFound();
            return Ok(trip);
        }

        [HttpPost]
        public IActionResult AddTrip([FromBody] Trip trip)
        {
            _tripService.AddTrip(trip);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTrip(int id, [FromBody] Trip trip)
        {
            _tripService.UpdateTrip(id, trip);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrip(int id)
        {
            var result = _tripService.DeleteTrip(id);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}