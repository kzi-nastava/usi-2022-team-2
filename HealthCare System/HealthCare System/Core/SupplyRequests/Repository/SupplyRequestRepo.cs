using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HealthCare_System.Core.SupplyRequests.Model;

namespace HealthCare_System.Core.SupplyRequests.Repository
{
    public class SupplyRequestRepo : ISupplyRequestRepo
    {
        List<SupplyRequest> supplyRequests;
        string path;

        public SupplyRequestRepo()
        {
            path = "../../../data/entities/SupplyRequests.json";
            Load();
        }

        public SupplyRequestRepo(string path)
        {
            this.path = path;
            Load();
        }

        internal List<SupplyRequest> SupplyRequests { get => supplyRequests; set => supplyRequests = value; }

        public string Path { get => path; set => path = value; }

        public void Load()
        {
            supplyRequests = JsonSerializer.Deserialize<List<SupplyRequest>>(File.ReadAllText(path));
        }

        public SupplyRequest FindById(int id)
        {
            foreach (SupplyRequest supplyRequest in supplyRequests)
                if (supplyRequest.Id == id)
                    return supplyRequest;
            return null;
        }

        public int GenerateId()
        {
            return supplyRequests[^1].Id + 1;
        }
        public void Serialize()
        {
            string supplyRequestsJson = JsonSerializer.Serialize(supplyRequests,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, supplyRequestsJson);
        }

        public void Add(SupplyRequest supplyRequest)
        {
            supplyRequests.Add(supplyRequest);
            Serialize();
        }

        public void Delete(SupplyRequest supplyRequest)
        {
            supplyRequests.Remove(supplyRequest);
            Serialize();
        }
    }
}
