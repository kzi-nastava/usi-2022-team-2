using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    abstract class Notification
    {
        int id;
        string message;

        protected Notification() { }

        protected Notification(int id, string message)
        {
            this.id = id;
            this.message = message;
        }

        protected Notification(Notification notification)
        {
            this.id = notification.Id;
            this.message = notification.Message;
        }

        [JsonPropertyName("message")]
        public string Message { get => message; set => message = value; }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }
    }
}
