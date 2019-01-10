// Developed by Softeq Development Corporation
// http://www.softeq.com

using Newtonsoft.Json;

namespace Softeq.NetKit.Services.PushNotifications.Models
{
    public class PushNotificationMessage
    {
        [JsonIgnore]
        public int NotificationType { get; set; }

        [JsonIgnore]
        public string Title { get; set; } = string.Empty;

        [JsonIgnore]
        public string Body { get; set; } = string.Empty;

        [JsonIgnore]
        public int Badge { get; set; } = 0;
        
        [JsonIgnore]
        public string Sound { get; set; } = "default";
        
        public virtual string GetData()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
