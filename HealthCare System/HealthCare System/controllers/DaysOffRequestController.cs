using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DaysOffRequestController
    {
        List<DaysOffRequest> daysOffRequests;

        public DaysOffRequestController()
        {
            Load();
        }

        public List<DaysOffRequest> DaysOffRequests
        {
            get { return daysOffRequests; }
            set { daysOffRequests = value; }
        }

        void Load()
        {
            daysOffRequests = JsonSerializer.Deserialize<List<DaysOffRequest>>(File.ReadAllText("data/entities/DaysOffRequests.json"));
        }

        public DaysOffRequest FindById(int id)
        {
            foreach (DaysOffRequest daysOffRequest in daysOffRequests)
                if (daysOffRequest.Id == id)
                    return daysOffRequest;
            return null;
        }
    }
}
