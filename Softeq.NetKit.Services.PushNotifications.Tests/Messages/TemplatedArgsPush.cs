using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Attributes;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.Messages
{
    public class TemplatedArgsPush : PushNotificationMessage
    {
        [JsonIgnore]
        [LocalizationParameter(LocalizationTarget.Body, 1)]
        [LocalizationParameter(LocalizationTarget.Title, 1)]
        public string Name { get; set; }

        [JsonIgnore]
        [LocalizationParameter(LocalizationTarget.Body, 2)]
        [LocalizationParameter(LocalizationTarget.Title, 2)]
        public string PostName { get; set; }

        public TemplatedArgsPush()
        {
            Title = "{0} liked your {1} post";
            Body = "{0} liked your {1} post. Check it out!";
            BodyLocalizationKey = "push_test_param_body";
            TitleLocalizationKey = "push_test_param_title";
            NotificationType = (int)PushNotificationType.PostLiked;
        }

        protected internal override string FormatBody()
        {
            return string.Format(Body, Name, PostName);
        }

        protected internal override string FormatTitle()
        {
            return string.Format(Title, Name, PostName);
        }
    }
}
