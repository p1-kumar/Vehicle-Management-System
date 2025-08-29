using VehicleManagement.Models;

namespace VehicleManagement.DataSource
{
    public class DataStore
    {
        public List<Vehicle> Vehicles { get; set; } = [];
        public List<Trip> Trips { get; set; } = [];
        public List<Maintenance> Maintenances { get; set; } = [];


    }
}
