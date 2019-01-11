// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Article
{
    public class PostLikedPush : PushNotificationMessage
    {
        [JsonProperty("postId")]
        public Guid PostId { get; set; }

        public PostLikedPush()
        {
            Title = "Post liked.";
            Body = "Someone liked your post. Check it out!";
            NotificationType = (int) PushNotificationType.PostLiked;
        }
    }
}
