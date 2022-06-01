﻿using System.Collections.Generic;
using HealthCare_System.Model;
using System.Text.Json;
using System.IO;

namespace HealthCare_System.controllers
{
    class SupplyRequestController
    {
        List<SupplyRequest> supplyRequests;
        string path;

        public SupplyRequestController()
        {
            path = "../../../data/entities/SupplyRequests.json";
            Load();
        }

        public SupplyRequestController(string path)
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
