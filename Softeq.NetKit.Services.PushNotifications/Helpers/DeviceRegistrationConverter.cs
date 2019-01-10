// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using Softeq.NetKit.Services.PushNotifications.Extensions;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Helpers
{
    internal static class DeviceRegistrationConverter
    {
        public static DeviceRegistration Convert(RegistrationDescription description)
        {
            var platform = description?.GetPlatform();
            if (platform == null)
            {
                return null;
            }

            return new DeviceRegistration
            {
                Tags = new List<string>(description.Tags),
                RegistrationId = description.RegistrationId,
                Platform = platform.Value
            };
        }
    }
}
