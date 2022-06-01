using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Repository.EquipmentRepo
{
    class SupplyRequestRepo
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

        void Load()
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
    }
}
