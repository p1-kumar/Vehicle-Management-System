namespace VehicleManagement.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double DistanceInKm { get; set; } 
        public string SourceLocation { get; set; } = string.Empty;
        public string DestinationLocation { get; set; } = string.Empty;
       
        
    }
}
