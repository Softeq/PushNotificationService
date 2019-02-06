using System;
using Newtonsoft.Json.Linq;
using Softeq.NetKit.Services.PushNotifications.Exception;
using Softeq.NetKit.Services.PushNotifications.Factories;
using Softeq.NetKit.Services.PushNotifications.Tests.Messages;
using Xunit;

namespace Softeq.NetKit.Services.PushNotifications.Tests
{
    public class PlatformNotificationFactoryTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void MissingContentTest()
        {
            var message = new PostLikedPush
            {
                Body = null, Title = null, BodyLocalizationKey = null, TitleLocalizationKey = null
            };

            Assert.Throws<ValidationException>(() => PlatformNotificationFactory.CreateTemplateProperties(message));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ContentFormattingTest()
        {
            var message = new TemplatedPush
            {
                Name = "Alex"
            };

            var push = PlatformNotificationFactory.CreateTemplateProperties(message);

            Assert.NotNull(push);
            Assert.Equal("Alex liked your post", push["title"]);
            Assert.Equal("Alex liked your post. Check it out!", push["body"]);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void PropertyPopulationTest()
        {
            var message = new PostLikedPush
            {
                PostId = Guid.NewGuid()
            };

            var push = PlatformNotificationFactory.CreateTemplateProperties(message);

            Assert.NotNull(push);
            Assert.Equal(8, push.Count);
            Assert.Equal(message.Title, push["title"]);
            Assert.Equal(message.Body, push["body"]);
            Assert.Equal(message.BodyLocalizationKey, push["body_loc_key"]);
            Assert.Equal(message.TitleLocalizationKey, push["title_loc_key"]);
            Assert.Equal(message.NotificationType.ToString(), push["type"]);
            Assert.Equal(message.Badge.ToString(), push["badge"]);
            Assert.Equal(message.Sound, push["sound"]);
            Assert.Equal(message.PostId, JObject.Parse(push["data"])["postId"]);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void LocalizationArgumentsPopulationTest()
        {
            var message = new TemplatedArgsPush
            {
                Name = "Alex",
                PostName = "SomePost"
            };

            var push = PlatformNotificationFactory.CreateTemplateProperties(message);

            Assert.NotNull(push);
            Assert.Equal(12, push.Count);
            Assert.Equal("Alex liked your SomePost post", push["title"]);
            Assert.Equal("Alex liked your SomePost post. Check it out!", push["body"]);
            Assert.Equal(message.BodyLocalizationKey, push["body_loc_key"]);
            Assert.Equal(message.TitleLocalizationKey, push["title_loc_key"]);
            Assert.Equal(message.NotificationType.ToString(), push["type"]);
            Assert.Equal(message.Badge.ToString(), push["badge"]);
            Assert.Equal(message.Sound, push["sound"]);
            Assert.Equal(message.Name, push["body_loc_arg1"]);
            Assert.Equal(message.PostName, push["body_loc_arg2"]);
            Assert.Equal(message.Name, push["title_loc_arg1"]);
            Assert.Equal(message.PostName, push["title_loc_arg2"]);
        }
    }
}
