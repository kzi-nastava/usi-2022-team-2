using HealthCare_System.Core.DaysOffRequests.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.DaysOffRequests.Repository
{
    public class DaysOffRequestRepo : IDaysOffRequestRepo
    {
        List<DaysOffRequest> daysOffRequests;
        string path;

        public DaysOffRequestRepo()
        {
            path = "../../../data/entities/DaysOffRequests.json";
            Load();
        }

        public DaysOffRequestRepo(string path)
        {
            this.path = path;
            Load();
        }

        internal List<DaysOffRequest> DaysOffRequests { get => daysOffRequests; set => daysOffRequests = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            daysOffRequests = JsonSerializer.Deserialize<List<DaysOffRequest>>(File.ReadAllText(path));
        }

        public DaysOffRequest FindById(int id)
        {
            foreach (DaysOffRequest daysOffRequest in daysOffRequests)
                if (daysOffRequest.Id == id)
                    return daysOffRequest;
            return null;
        }

        public void Serialize()
        {
            string daysOffRequestsJson = JsonSerializer.Serialize(daysOffRequests,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, daysOffRequestsJson);
        }
    }
}
