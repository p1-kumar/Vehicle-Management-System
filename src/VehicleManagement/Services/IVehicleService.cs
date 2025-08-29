using VehicleManagement.Models;

namespace VehicleManagement.Services
{
    public interface IVehicleService
    {
        List<Vehicle> GetAllVehicles();

        Vehicle? GetVehicleById(int id);

        void AddVehicle(Vehicle vehicle);

        void UpdateVehicle(int id, Vehicle vehicle);

        bool DeleteVehicle(int id);
    }
}
