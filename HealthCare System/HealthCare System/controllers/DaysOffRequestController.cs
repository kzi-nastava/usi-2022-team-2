using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class DaysOffRequestController
    {
        List<DaysOffRequest> daysOffRequests;

        public DaysOffRequestController()
        {
            this.LoadDaysOffRequests();
        }

        public List<DaysOffRequest> DaysOffRequests
        {
            get { return daysOffRequests; }
            set { daysOffRequests = value; }
        }

        void LoadDaysOffRequests()
        {
            this.daysOffRequests = JsonSerializer.Deserialize<List<DaysOffRequest>>(File.ReadAllText("data/entities/DaysOffRequests.json"));
        }

        public DaysOffRequest FindById(int id)
        {
            foreach (DaysOffRequest daysOffRequest in this.daysOffRequests)
                if (daysOffRequest.Id == id)
                    return daysOffRequest;
            return null;
        }
    }
}
