using VehicleManagement.DataSource;
using VehicleManagement.Models;
using Xunit;

namespace VehicleManagement.Tests.DataSource
{
    public class DataStoreTests
    {
        [Fact]
        public void VehiclesList_ShouldBeEmpty_OnInit()
        {
            var store = new DataStore();
            Assert.Empty(store.Vehicles);
        }

        [Fact]
        public void CanAddVehicle_ToVehiclesList()
        {
            var store = new DataStore();
            var vehicle = new Vehicle { Id = 1, Name = "Car" };
            store.Vehicles.Add(vehicle);
            Assert.Single(store.Vehicles);
            Assert.Equal("Car", store.Vehicles[0].Name);
        }

        [Fact]
        public void CanAddTrip_ToTripsList()
        {
            var store = new DataStore();
            var trip = new Trip { Id = 1, VehicleId = 1 };
            store.Trips.Add(trip);
            Assert.Single(store.Trips);
            Assert.Equal(1, store.Trips[0].VehicleId);
        }
    }
}
