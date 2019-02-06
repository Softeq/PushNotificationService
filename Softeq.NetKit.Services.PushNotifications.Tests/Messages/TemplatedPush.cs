using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.Messages
{
    public class TemplatedPush : PushNotificationMessage
    {
        [JsonIgnore]
        public string Name { get; set; }

        public TemplatedPush()
        {
            Title = "{0} liked your post";
            Body = "{0} liked your post. Check it out!";
            NotificationType = (int)PushNotificationType.PostLiked;
        }

        protected internal override string FormatBody()
        {
            return string.Format(Body, Name);
        }

        protected internal override string FormatTitle()
        {
            return string.Format(Title, Name);
        }
    }
}
