// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Linq;
using System.Threading.Tasks;
using Softeq.NetKit.Services.PushNotifications.Client;
using Softeq.NetKit.Services.PushNotifications.Models;
using Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Comment;
using Xunit;

namespace Softeq.NetKit.Services.PushNotifications.Tests
{
    public class NotificationHubRegistrationIntegrationTest
    {
        private const string _connectionString = "INSERT_CONN_STR";
        private const string _hubName = "INSERT_HUB_NAME";
        private const string _testUserId = "test@test.test";
        private const string _apnsDeviceToken = "INSERT_TOKEN";

        private readonly AzureNotificationHubSender _hubSender;
        private readonly AzureNotificationHubSubscriber _hubSubscriber;
        private readonly Microsoft.Azure.NotificationHubs.NotificationHubClient _nativeHub;

        public NotificationHubRegistrationIntegrationTest()
        {
            _hubSender = new AzureNotificationHubSender(new AzureNotificationHubConfiguration(_connectionString, _hubName));
            _hubSubscriber = new AzureNotificationHubSubscriber(new AzureNotificationHubConfiguration(_connectionString, _hubName));
            _nativeHub = Microsoft.Azure.NotificationHubs.NotificationHubClient.CreateClientFromConnectionString(_connectionString, _hubName);
        }
        
        [Fact]
        public async Task ShouldCreateRegistration()
        {
            await _hubSubscriber.CreateOrUpdatePushSubscriptionAsync(new PushSubscriptionRequest(_apnsDeviceToken, PushPlatformEnum.iOS, new[] {_testUserId}));
            var registrations = await _nativeHub.GetRegistrationsByChannelAsync(_apnsDeviceToken, 100);
            Assert.NotNull(registrations);
            Assert.Equal(_testUserId, registrations.First().Tags.First());
        }
        
        [Fact]
        public async Task ShouldUnsubscribeUserFromPush()
        {
            await ShouldCreateRegistration();
            
            await _hubSubscriber.UnsubscribeByTagAsync(_testUserId);
            var registrations = await _nativeHub.GetRegistrationsByChannelAsync(_apnsDeviceToken, 100);
            Assert.NotNull(registrations);
            Assert.Empty(registrations);
        }
        
        [Fact]
        public async Task ShouldUnsubscribeDeviceFromPush()
        {
            await ShouldCreateRegistration();
            
            await _hubSubscriber.UnsubscribeDeviceAsync(_apnsDeviceToken);
            var registrations = await _nativeHub.GetRegistrationsByChannelAsync(_apnsDeviceToken, 100);
            Assert.NotNull(registrations);
            Assert.Empty(registrations);
        }
        
        [Fact]
        public async Task ShouldSendToSingleUserAsync()
        {
            await ShouldCreateRegistration();
            
            await _hubSender.SendAsync(new CommentLikedPush(), _testUserId);
            
            await ShouldUnsubscribeUserFromPush();
        }
    }
}