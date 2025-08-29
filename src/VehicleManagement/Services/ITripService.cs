using VehicleManagement.Models;

namespace VehicleManagement.Services
{
    public interface ITripService
    {
        Trip? GetTrip(int id);

        List<Trip> GetTripsByVehicleId(int vehicleId);

        void AddTrip(Trip vehicle);

        void UpdateTrip(int id, Trip trip);

        bool DeleteTrip(int id);
    }
}
