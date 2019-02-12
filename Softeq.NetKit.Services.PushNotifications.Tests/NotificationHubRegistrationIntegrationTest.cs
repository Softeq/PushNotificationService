// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.NetKit.Services.PushNotifications.Client;
using Softeq.NetKit.Services.PushNotifications.Models;
using Softeq.NetKit.Services.PushNotifications.Tests.Messages;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Softeq.NetKit.Services.PushNotifications.Tests
{
    public class NotificationHubRegistrationIntegrationTest : IClassFixture<ServiceTestFixture>
    {
        private readonly string _testUserId = "test@test.test";
        private readonly ServiceTestFixture _fixture;
        private readonly AzureNotificationHubSender _hubSender;
        private readonly AzureNotificationHubSubscriber _hubSubscriber;
        private readonly Microsoft.Azure.NotificationHubs.NotificationHubClient _nativeHub;

        public NotificationHubRegistrationIntegrationTest(ServiceTestFixture fixture)
        {
            _fixture = fixture;
            _hubSender = new AzureNotificationHubSender(_fixture.HubConfig);
            _hubSubscriber = new AzureNotificationHubSubscriber(_fixture.HubConfig);
            _nativeHub = Microsoft.Azure.NotificationHubs.NotificationHubClient.CreateClientFromConnectionString(_fixture.HubConfig.ConnectionString, _fixture.HubConfig.HubName);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldCreateRegistration()
        {
            await _hubSubscriber.CreateOrUpdatePushSubscriptionAsync(new PushSubscriptionRequest(_fixture.ApnsDeviceToken, PushPlatformEnum.iOS, new[] { _testUserId }));
            var registrations = await _nativeHub.GetRegistrationsByChannelAsync(_fixture.ApnsDeviceToken, 100);
            Assert.NotNull(registrations);
            Assert.Equal(_testUserId, registrations.First().Tags.First());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldUnsubscribeUserFromPush()
        {
            await ShouldCreateRegistration();

            await _hubSubscriber.UnsubscribeByTagAsync(_testUserId);
            var registrations = await _nativeHub.GetRegistrationsByChannelAsync(_fixture.ApnsDeviceToken, 100);
            Assert.NotNull(registrations);
            Assert.Empty(registrations);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldUnsubscribeDeviceFromPush()
        {
            await ShouldCreateRegistration();

            await _hubSubscriber.UnsubscribeDeviceAsync(_fixture.ApnsDeviceToken);
            var registrations = await _nativeHub.GetRegistrationsByChannelAsync(_fixture.ApnsDeviceToken, 100);
            Assert.NotNull(registrations);
            Assert.Empty(registrations);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldGetRegistrationByTag()
        {
            await ShouldCreateRegistration();

            var existingRegistrations = await _hubSubscriber.GetRegistrationsByTagAsync(_testUserId);

            var registrations = await _nativeHub.GetRegistrationsByChannelAsync(_fixture.ApnsDeviceToken, 100);
            Assert.NotNull(registrations);
            Assert.True(registrations.Count() == existingRegistrations.Count());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldSendToSingleUserAsync()
        {
            await ShouldCreateRegistration();

            await _hubSender.SendAsync(new CommentLikedPush
            {
                Body = "Template Body",
                Title = "Template Title",
            }, _testUserId);

            await ShouldUnsubscribeUserFromPush();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldSendTemplatedNotification()
        {
            await ShouldCreateRegistration();

            var pushNotificationMessage = new TemplatedArgsPush
            {
                Name = "Alex",
                PostName = "BitcoinPost"
            };

            //act
            await _hubSender.SendAsync(pushNotificationMessage, _testUserId);

            await ShouldUnsubscribeUserFromPush();
        }
    }
}