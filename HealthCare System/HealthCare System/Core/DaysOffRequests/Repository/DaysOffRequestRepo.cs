using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.Users.Model;
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

        public List<DaysOffRequest> DaysOffRequests { get => daysOffRequests; set => daysOffRequests = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            daysOffRequests = JsonSerializer.Deserialize<List<DaysOffRequest>>(File.ReadAllText(path));
        }

        public int GenerateId()
        {
            return daysOffRequests[^1].Id + 1;
        }

        public DaysOffRequest FindById(int id)
        {
            foreach (DaysOffRequest daysOffRequest in daysOffRequests)
                if (daysOffRequest.Id == id)
                    return daysOffRequest;
            return null;
        }

        public void Serialize(string linkPath = "../../../data/links/DaysOffRequest_Doctor.csv")
        {
            string daysOffRequestsJson = JsonSerializer.Serialize(daysOffRequests,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, daysOffRequestsJson);

            string csv = "";

            foreach (DaysOffRequest daysOffRequest in daysOffRequests)
                csv += daysOffRequest.Id.ToString() + ";" + daysOffRequest.Doctor.Jmbg + "\n";

            File.WriteAllText(linkPath, csv);
        }

        public List<DaysOffRequest> FillterByDoctor(Doctor doctor)
        {
            List<DaysOffRequest> fillteredRequests = new List<DaysOffRequest>();

            foreach (DaysOffRequest daysOffRequest in daysOffRequests)
                if (daysOffRequest.Doctor.Jmbg == doctor.Jmbg)
                    fillteredRequests.Add(daysOffRequest);

            return fillteredRequests;
        }

        public void Add(DaysOffRequest daysOffRequest)
        {
            daysOffRequests.Add(daysOffRequest);
            Serialize();
        }
    }
}
