using System.Net.Sockets;
using Microsoft.Azure.NotificationHubs.Messaging;

namespace Softeq.NetKit.Services.PushNotifications.Extensions
{
    internal static class RetryPolicyExceptionExtensions
    {
        public static bool IsTransient(this System.Exception exception)
        {
            return exception is MessagingException messagingException && messagingException.IsTransient ||
                   exception is SocketException;
        }
    }
}
