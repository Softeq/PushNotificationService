// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using EnsureThat;

namespace Softeq.NetKit.Services.PushNotifications.Client
{
    public class AzureNotificationHubConfiguration
    {
        public AzureNotificationHubConfiguration(
            string connectionString,
            string hubName,
            TimeSpan[] transientErrorRetryDelays = null)
        {
            ConnectionString = Ensure.String.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));
            HubName = Ensure.String.IsNotNullOrWhiteSpace(hubName, nameof(hubName));
            if (TransientErrorRetryDelays != null)
            {
                TransientErrorRetryDelays = Ensure.Collection.HasItems(transientErrorRetryDelays, nameof(transientErrorRetryDelays));
            }
        }

        public string ConnectionString { get; }

        public string HubName { get; }

        public TimeSpan[] TransientErrorRetryDelays { get; }
    }
}
