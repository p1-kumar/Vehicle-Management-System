using VehicleManagement.Models;

namespace VehicleManagement.Services
{
    public interface IMaintenanceService
    {
        bool ScheduleMaintenance(int id, DateTime dateTime);

        Maintenance? GetMaintenanceByVehicleId(int vehicleId);
    }
}
