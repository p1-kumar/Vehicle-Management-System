namespace VehicleManagement.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
