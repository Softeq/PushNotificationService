// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;

namespace Softeq.NetKit.Services.PushNotifications.Models
{
    public class DeviceRegistration
    {
        public PushPlatformEnum Platform { get; set; }
        public string RegistrationId { get; set; }
        public IList<string> Tags { get; set; }
    }
}
