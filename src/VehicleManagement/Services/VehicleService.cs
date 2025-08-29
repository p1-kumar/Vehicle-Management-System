using VehicleManagement.DataSource;
using VehicleManagement.Models;

namespace VehicleManagement.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly List<Vehicle> Vehicles;

        private readonly IDataStore _dataStore;

        public VehicleService(IDataStore dataStore)
        {
            _dataStore = dataStore;
            dataStore.LoadData();
            Vehicles = dataStore.Records.Vehicles;
        }
        public void AddVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
            _dataStore.SaveData();
        }

        public bool DeleteVehicle(int id)
        {
            var isRemoved =  Vehicles.RemoveAll(v => v.Id == id) > 0;
            if (isRemoved)
            {
                _dataStore.SaveData();
            }
            return isRemoved;
        }

        public List<Vehicle> GetAllVehicles()
        {
            return Vehicles;
        }

        public Vehicle? GetVehicleById(int id)
        {
            return Vehicles.FirstOrDefault(v => v.Id == id);

        }

        public void UpdateVehicle(int id, Vehicle vehicle)
        {
            var existing = Vehicles.FirstOrDefault(v => v.Id == id) ?? throw new KeyNotFoundException($"Vehicle with Id {id} not found.");
            existing.Name = vehicle.Name;
            existing.Description = vehicle.Description;
            existing.VehicleType = vehicle.VehicleType;
            _dataStore.SaveData();
        }
    }
}
