using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    class SupplyRequest
    {
        int id;
        DateTime requestCreated;
        Dictionary<Equipment, int> orderDetails;

        public SupplyRequest() { }

        public SupplyRequest(int id, DateTime requestCreated)
        {
            this.id = id;
            this.requestCreated = requestCreated;
        }

        public SupplyRequest(int id, DateTime requestCreated, Dictionary<Equipment, int> orderDetails)
        {
            this.id = id;
            this.requestCreated = requestCreated;
            this.orderDetails = orderDetails;
        }

        public SupplyRequest(SupplyRequest request)
        {
            this.id = request.id;
            this.requestCreated = request.requestCreated;
            this.orderDetails = request.orderDetails;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }
        [JsonPropertyName("requestCreated")]
        public DateTime RequestCreated { get => requestCreated; set => requestCreated = value; }
        [JsonIgnore]
        internal Dictionary<Equipment, int> OrderDetails { get => orderDetails; set => orderDetails = value; }

        public override string ToString()
        {
            string orders="{";
            bool firstEntry = true;
            foreach (KeyValuePair<Equipment, int> equipment in this.orderDetails)
            {
                if (firstEntry) firstEntry = false;
                else orders += ", ";
                orders += equipment.Key + ":" + equipment.Value;
            }
            orders += "}";
            return "DrugNotification[" + "id: " + this.Id.ToString() +
                ", requestCreated: " + this.requestCreated.ToString("dd/MM/yyyy HH:mm") + ", orderDetails: " + orders + "]";
        }
    }
}
