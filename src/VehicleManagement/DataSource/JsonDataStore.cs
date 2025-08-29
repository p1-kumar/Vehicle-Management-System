using Newtonsoft.Json;

namespace VehicleManagement.DataSource
{
    public class JsonDataStore(string filePath) : IDataStore
    {
        private readonly string _filePath = filePath;

        public DataStore Records { get; set; } = new DataStore();

        public void LoadData()
        {
            try
            {
                using var reader = new StreamReader(_filePath);
                var json = reader.ReadToEnd();
                Records = JsonConvert.DeserializeObject<DataStore>(json) ?? new DataStore();
            }
            catch (FileNotFoundException)
            {
                // File not found, initialize empty records
                throw new IOException($"Data file not found at {_filePath}.", new FileNotFoundException());
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions as needed
                throw new IOException($"Error loading data from {_filePath}: {ex.Message}", ex);
            }
        }

        public void SaveData()
        {
            try
            {
                if (Records != null)
                {
                    var json = JsonConvert.SerializeObject(Records, Formatting.Indented);
                    using var writer = new StreamWriter(_filePath, false);
                    writer.Write(json);
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new IOException($"Error saving data to {_filePath}: {ex.Message}", ex);
            }
        }
    }
}
