// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.Messages
{
    public class PostLikedPush : PushNotificationMessage
    {
        [JsonProperty("postId")]
        public Guid PostId { get; set; }

        public PostLikedPush()
        {
            Title = "Post liked.";
            Body = "Someone liked your post. Check it out!";
            TitleLocalizationKey = "post_liked_title";
            BodyLocalizationKey = "post_liked_body";
            NotificationType = (int) PushNotificationType.PostLiked;
        }
    }
}
