// Developed by Softeq Development Corporation
// http://www.softeq.com

using EnsureThat;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using Softeq.NetKit.Services.PushNotifications.Abstractions;
using Softeq.NetKit.Services.PushNotifications.Exception;
using Softeq.NetKit.Services.PushNotifications.Factories;
using Softeq.NetKit.Services.PushNotifications.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softeq.NetKit.Services.PushNotifications.Helpers;

namespace Softeq.NetKit.Services.PushNotifications.Client
{
    public class AzureNotificationHubSubscriber : IPushNotificationSubscriber
    {
        private readonly NotificationHubClient _hub;

        public AzureNotificationHubSubscriber(AzureNotificationHubConfiguration configuration)
        {
            Ensure.That(configuration, nameof(configuration)).IsNotNull();
            _hub = NotificationHubClient.CreateClientFromConnectionString(configuration.ConnectionString, configuration.HubName);
        }

        public async Task CreateOrUpdatePushSubscriptionAsync(PushSubscriptionRequest request)
        {
            Ensure.That(request, nameof(request)).IsNotNull();

            await DeleteRegistrationsForDeviceAsync(request.DeviceHandle);
            await CreateRegistrationAsync(request);
        }

        public async Task UnsubscribeDeviceAsync(string deviceHandle)
        {
            Ensure.That(deviceHandle, nameof(deviceHandle)).IsNotNullOrWhiteSpace();

            await DeleteRegistrationsForDeviceAsync(deviceHandle);
        }

        public async Task UnsubscribeByTagAsync(string tag)
        {
            Ensure.That(tag, nameof(tag)).IsNotNullOrWhiteSpace();

            var registrations = await GetRegistrationsForTagAsync(tag);
            try
            {
                foreach (var registration in registrations)
                {
                    await _hub.DeleteRegistrationAsync(registration);
                }
            }
            catch (MessagingException ex)
            {
                throw new PushNotificationException("Something went wrong during deleting token request", ex);
            }
        }

        public async Task<IEnumerable<DeviceRegistration>> GetRegistrationsByTagAsync(string tag)
        {
            Ensure.That(tag, nameof(tag)).IsNotNullOrWhiteSpace();
            var registrations = await GetRegistrationsForTagAsync(tag);
            return registrations.Select(DeviceRegistrationConverter.Convert).Where(x => x != null).ToList();
        }

        private async Task CreateRegistrationAsync(PushSubscriptionRequest deviceUpdate)
        {
            var registration = PushRegistrationsFactory.CreateTemplateRegistration(deviceUpdate);

            try
            {
                await _hub.CreateRegistrationAsync(registration);
            }
            catch (MessagingException e)
            {
                throw new PushNotificationException("Error while trying to create device token registration in notification hub", e);
            }
        }

        private async Task DeleteRegistrationsForDeviceAsync(string pnsHandle)
        {
            var isDeleted = false;
            while (!isDeleted)
            {
                try
                {
                    await _hub.DeleteRegistrationsByChannelAsync(pnsHandle);
                    isDeleted = true;
                }
                catch (MessagingEntityNotFoundException)
                {
                    // Intentionally doing nothing here
                    // Internally, Azure SDK will delete registrations concurrently with Task.WhenAll.
                    // If at least one deletion fails (e.g. due to a race condition with other delete operation, 404 response is returned),
                    // we need to retry to ensure everything is deleted
                }
                catch (MessagingException ex)
                {
                    throw new PushNotificationException($"Something went wrong during deleting registrations for device handle: '{pnsHandle}'.", ex);
                }
            }
        }

        private async Task<IEnumerable<RegistrationDescription>> GetRegistrationsForTagAsync(string tagValue)
        {
            try
            {
                const int pageSize = 100;
                var allRegistrationDescriptions = new List<RegistrationDescription>();
                string continuationToken = string.Empty;

                do
                {
                    var registrations = await _hub.GetRegistrationsByTagAsync(tagValue, continuationToken, pageSize);
                    allRegistrationDescriptions.AddRange(registrations);
                    continuationToken = registrations.ContinuationToken;

                } while (!string.IsNullOrWhiteSpace(continuationToken));

                return allRegistrationDescriptions;
            }
            catch (MessagingException ex)
            {
                throw new PushNotificationException("Something went wrong while retrieving device registration list", ex);
            }
        }
    }
}
