// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Abstractions
{
    public interface IPushNotificationSender
    {
        Task<bool> SendAsync(PushNotificationMessage message, List<string> includeTags, List<string> excludeTags = null);
        Task<bool> SendAsync(PushNotificationMessage message, string tag);
    }
}