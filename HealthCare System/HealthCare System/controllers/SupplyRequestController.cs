using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;

namespace HealthCare_System.controllers
{
    class SupplyRequestController
    {
        List<SupplyRequest> supplyRequests;

        public SupplyRequestController()
        {
            Load();
        }

        public List<SupplyRequest> SupplyRequests
        {
            get { return supplyRequests; }
            set { supplyRequests = value; }
        }

        void Load()
        {
            supplyRequests = JsonSerializer.Deserialize<List<SupplyRequest>>(File.ReadAllText("data/entities/SupplyRequests.json"));
        }

        public SupplyRequest FindById(int id)
        {
            foreach (SupplyRequest supplyRequest in supplyRequests)
                if (supplyRequest.Id == id)
                    return supplyRequest;
            return null;
        }
    }
}
