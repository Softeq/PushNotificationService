// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Abstractions
{
    public interface IPushNotificationSubscriber
    {
        Task CreateOrUpdatePushSubscriptionAsync(PushSubscriptionRequest request);
        Task<IEnumerable<DeviceRegistration>> GetRegistrationsByTagAsync(string tag);
        Task UnsubscribeDeviceAsync(string deviceHandle);
        Task UnsubscribeByTagAsync(string tag);
    }
}