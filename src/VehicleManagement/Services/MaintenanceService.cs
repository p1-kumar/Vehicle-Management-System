using VehicleManagement.DataSource;
using VehicleManagement.Models;

namespace VehicleManagement.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly List<Maintenance> Maintenances;
        private readonly IDataStore _dataStore;

        public MaintenanceService(IDataStore dataStore)
        {
            _dataStore = dataStore;
            dataStore.LoadData();
            Maintenances = dataStore.Records.Maintenances;
        }
        public Maintenance? GetMaintenanceByVehicleId(int vehicleId)
        {
            return Maintenances.FirstOrDefault(m => m.VehicleId == vehicleId);
        }

        public bool ScheduleMaintenance(int id, DateTime dateTime)
        {
            var maintenance = Maintenances.FirstOrDefault(m => m.Id == id);
            if (maintenance == null)
                return false;           
            maintenance.ScheduledDate = dateTime;
            _dataStore.SaveData();
            return true;

        }
    }
}