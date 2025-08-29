namespace VehicleManagement.DataSource
{
    public interface IDataStore
    {
        void LoadData();
        void SaveData();
        DataStore Records { get; set; }
    }
}
