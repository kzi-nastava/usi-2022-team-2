using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.LoadSupplyRequests();
        }

        public List<SupplyRequest> SupplyRequests
        {
            get { return supplyRequests; }
            set { supplyRequests = value; }
        }

        void LoadSupplyRequests()
        {
            this.supplyRequests = JsonSerializer.Deserialize<List<SupplyRequest>>(File.ReadAllText("data/entities/SupplyRequests.json"));
        }

        public SupplyRequest FindById(int id)
        {
            foreach (SupplyRequest supplyRequest in this.supplyRequests)
                if (supplyRequest.Id == id)
                    return supplyRequest;
            return null;
        }
    }
}
