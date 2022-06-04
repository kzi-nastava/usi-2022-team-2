using System.Text.Json.Serialization;

namespace HealthCare_System.Model
{
    public abstract class Notification
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
            id = notification.Id;
            message = notification.Message;
        }

        [JsonPropertyName("message")]
        public string Message { get => message; set => message = value; }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }
    }
}
