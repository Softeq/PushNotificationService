// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using EnsureThat;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using Softeq.NetKit.Services.PushNotifications.Abstractions;
using Softeq.NetKit.Services.PushNotifications.Exception;
using Softeq.NetKit.Services.PushNotifications.Factories;
using Softeq.NetKit.Services.PushNotifications.Helpers;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Client
{
    public class AzureNotificationHubSender : IPushNotificationSender
    {
        private readonly NotificationHubClient _hub;

        public AzureNotificationHubSender(AzureNotificationHubConfiguration configuration)
        {
            Ensure.That(configuration, nameof(configuration)).IsNotNull();
            _hub = new NotificationHubClient(configuration.ConnectionString, configuration.HubName, new NotificationHubSettings
            {
                RetryOptions = configuration.NotificationHubRetryOptions
            });
        }

        public Task<bool> SendAsync(PushNotificationMessage message, string tag)
        {
            Ensure.That(message, nameof(message)).IsNotNull();
            Ensure.That(tag, nameof(tag)).IsNotNullOrWhiteSpace();

            return TrySendAsync(message, tag);
        }

        public Task<bool> SendAsync(PushNotificationMessage message, List<string> includeTags, List<string> excludeTags = null)
        {
            Ensure.That(message, nameof(message)).IsNotNull();
            Ensure.That(includeTags, nameof(includeTags)).IsNotNull();

            var notificationExpressionString = TagExpressionGenerator.GetExpressionString(includeTags, excludeTags);
            return TrySendAsync(message, notificationExpressionString);
        }

        private async Task<bool> TrySendAsync(PushNotificationMessage message, string tagExpression)
        {
            var notification = PlatformNotificationFactory.CreateTemplateProperties(message);

            try
            {
                var outcome = await _hub.SendTemplateNotificationAsync(notification, tagExpression);
                if (outcome == null)
                {
                    return false;
                }

                return !(outcome.State == NotificationOutcomeState.Abandoned ||
                         outcome.State == NotificationOutcomeState.Unknown);
            }
            catch (MessagingException ex)
            {
                throw new PushNotificationException("Error while sending push notification", ex);
            }
        }
    }
}