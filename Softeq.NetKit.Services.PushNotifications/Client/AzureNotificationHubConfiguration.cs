// Developed by Softeq Development Corporation
// http://www.softeq.com

using EnsureThat;
using Microsoft.Azure.NotificationHubs;

namespace Softeq.NetKit.Services.PushNotifications.Client
{
    public class AzureNotificationHubConfiguration
    {
        public AzureNotificationHubConfiguration(
            string connectionString,
            string hubName,
            NotificationHubRetryOptions notificationHubRetryOptions = null)
        {
            ConnectionString = Ensure.String.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));
            HubName = Ensure.String.IsNotNullOrWhiteSpace(hubName, nameof(hubName));
            NotificationHubRetryOptions = notificationHubRetryOptions ?? new NotificationHubRetryOptions();
        }

        public string ConnectionString { get; }

        public string HubName { get; }

        public NotificationHubRetryOptions NotificationHubRetryOptions { get; }
    }
}
