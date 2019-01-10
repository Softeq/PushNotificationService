// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.NetKit.Services.PushNotifications.Models;
using System.Collections.Generic;

namespace Softeq.NetKit.Services.PushNotifications.Factories
{
    internal static class PlatformNotificationFactory
    {
        public static IDictionary<string, string> CreateTemplateProperties(PushNotificationMessage message)
        {
            var properties = new Dictionary<string, string>
            {
                {"body", message.Body},
                {"title", message.Title},
                {"sound", message.Sound},
                {"badge", message.Badge.ToString()},
                {"type", message.NotificationType.ToString()},
                {"data", message.GetData()}
            };

            return properties;
        }
    }
}
