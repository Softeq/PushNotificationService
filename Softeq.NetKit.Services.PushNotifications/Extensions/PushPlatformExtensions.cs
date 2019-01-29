// Developed by Softeq Development Corporation
// http://www.softeq.com

using Microsoft.Azure.NotificationHubs;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Extensions
{
    internal static class PushPlatformExtensions
    {
        public static bool TryGetPlatform(this RegistrationDescription registration, out PushPlatformEnum platform)
        {
            switch (registration)
            {
                case AppleRegistrationDescription a:
                    platform = PushPlatformEnum.iOS;
                    return true;
                case GcmRegistrationDescription g:
                    platform = PushPlatformEnum.Android;
                    return true;
                default:
                    platform = PushPlatformEnum.Android;
                    return false;
            }
        }
    }
}
