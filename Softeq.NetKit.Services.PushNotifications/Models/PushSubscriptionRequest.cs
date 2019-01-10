// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;

namespace Softeq.NetKit.Services.PushNotifications.Models
{
    public class PushSubscriptionRequest
    {
        public PushSubscriptionRequest(string deviceHandle, PushPlatformEnum platform, IEnumerable<string> tags)
        {
            DeviceHandle = deviceHandle;
            Platform = platform;
            Tags = tags;
        }
        public PushPlatformEnum Platform { get; set; }
        public string DeviceHandle { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
