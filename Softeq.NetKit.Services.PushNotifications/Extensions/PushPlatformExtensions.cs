// Developed by Softeq Development Corporation
// http://www.softeq.com

using Microsoft.Azure.NotificationHubs;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Extensions
{
    internal static class PushPlatformExtensions
    {
        public static PushPlatformEnum? GetPlatform(this RegistrationDescription registration)
        {
            switch (registration)
            {
                case AppleRegistrationDescription a:
                    return PushPlatformEnum.iOS;
                case FcmRegistrationDescription f:
                    return PushPlatformEnum.Android;
                default:
                    return null;
            }
        }
    }
}
