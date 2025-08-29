using VehicleManagement.DataSource;
using VehicleManagement.Models;

namespace VehicleManagement.Services
{
    public class TripService : ITripService
    {
        private readonly List<Trip> Trips;

        private readonly IDataStore _dataStore;

        public TripService(IDataStore dataStore)
        {
            _dataStore = dataStore;
            dataStore.LoadData();
            Trips = dataStore.Records.Trips;
        }

        public void AddTrip(Trip trip)
        {
            Trips.Add(trip);
            _dataStore.SaveData();
        }

        public bool DeleteTrip(int id)
        {
            var isRemoved = Trips.RemoveAll(t => t.Id == id) > 0;
            if(isRemoved)
            {   
                 _dataStore.SaveData();
            }
            return isRemoved;
        }

        public Trip? GetTrip(int id)
        {
            return Trips.FirstOrDefault(t => t.Id == id);
        }

        public List<Trip> GetTripsByVehicleId(int vehicleId)
        {
            return [.. Trips.Where(t => t.VehicleId == vehicleId)];
        }

        public void UpdateTrip(int id, Trip trip)
        {
            var existing = Trips.FirstOrDefault(t => t.Id == id);
            if (existing == null)
                throw new KeyNotFoundException($"Trip with Id {id} not found.");
            existing.StartTime = trip.StartTime;
            existing.EndTime = trip.EndTime;
            existing.DistanceInKm = trip.DistanceInKm;
            existing.SourceLocation = trip.SourceLocation;
            existing.DestinationLocation = trip.DestinationLocation;
            existing.VehicleId = trip.VehicleId;

            _dataStore.SaveData();
        }
    }
}
