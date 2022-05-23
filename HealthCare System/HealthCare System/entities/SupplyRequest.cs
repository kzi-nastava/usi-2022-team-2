using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class SupplyRequest
    {
        int id;
        DateTime requestCreated;
        Dictionary<Equipment, int> orderDetails;

        public SupplyRequest() 
        {
            orderDetails = new Dictionary<Equipment, int>();
        }

        public SupplyRequest(int id, Equipment equipment, int quantity)
        {
            this.id = id;
            this.requestCreated = DateTime.Now;
            this.orderDetails = new Dictionary<Equipment, int>();
            this.orderDetails[equipment] = quantity;
        }

        public SupplyRequest(int id, DateTime requestCreated)
        {
            this.id = id;
            this.requestCreated = requestCreated;
            orderDetails = new Dictionary<Equipment, int>();
        }

        public SupplyRequest(int id, DateTime requestCreated, Dictionary<Equipment, int> orderDetails)
        {
            this.id = id;
            this.requestCreated = requestCreated;
            this.orderDetails = orderDetails;
        }

        public SupplyRequest(SupplyRequest request)
        {
            id = request.id;
            requestCreated = request.requestCreated;
            orderDetails = request.orderDetails;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("requestCreated")]
        public DateTime RequestCreated { get => requestCreated; set => requestCreated = value; }

        [JsonIgnore]
        public Dictionary<Equipment, int> OrderDetails { get => orderDetails; set => orderDetails = value; }

        public override string ToString()
        {
            string orders="{";
            bool firstEntry = true;

            foreach (KeyValuePair<Equipment, int> equipment in orderDetails)
            {
                if (firstEntry) firstEntry = false;
                else orders += ", ";
                orders += equipment.Key + ":" + equipment.Value;
            }

            orders += "}";
            return "SupplyRequest[" + "id: " + Id.ToString() + ", requestCreated: " 
                + requestCreated.ToString("dd/MM/yyyy HH:mm") + ", orderDetails: " + orders + "]";
        }
    }
}
