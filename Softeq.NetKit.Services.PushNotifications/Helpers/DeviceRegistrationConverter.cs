// Developed by Softeq Development Corporation
// http://www.softeq.com

using Microsoft.Azure.NotificationHubs;
using Softeq.NetKit.Services.PushNotifications.Extensions;
using Softeq.NetKit.Services.PushNotifications.Models;
using System.Collections.Generic;

namespace Softeq.NetKit.Services.PushNotifications.Helpers
{
    internal static class DeviceRegistrationConverter
    {
        public static DeviceRegistration Convert(RegistrationDescription description)
        {
            if (description == null || !description.TryGetPlatform(out var platform))
            {
                return null;
            }

            var deviceRegistration = new DeviceRegistration
            {
                Tags = new List<string>(description.Tags),
                RegistrationId = description.RegistrationId,
                Platform = platform,
                PnsHandle = RetrieveDeviceHandle(platform, description)
            };

            return deviceRegistration;
        }

        private static string RetrieveDeviceHandle(PushPlatformEnum platform, RegistrationDescription description)
        {
            switch (platform)
            {
                case PushPlatformEnum.iOS:
                {
                    return ((AppleTemplateRegistrationDescription)description).DeviceToken;
                }
                case PushPlatformEnum.Android:
                {
                    return ((FcmTemplateRegistrationDescription)description).FcmRegistrationId;
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
