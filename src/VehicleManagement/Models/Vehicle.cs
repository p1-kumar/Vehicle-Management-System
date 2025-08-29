using System.ComponentModel;

namespace VehicleManagement.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public VehicleTypeEnum VehicleType { get; set; }
    }

    public enum VehicleTypeEnum
    {
        None = 0,
        TwoWheeler = 1,
        ThreeWheeler = 2,
        FourWheeler = 3,
        HeavyVehicle = 4
    }
}
