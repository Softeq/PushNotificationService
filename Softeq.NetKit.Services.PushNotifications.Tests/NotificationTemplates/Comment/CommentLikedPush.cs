// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Comment
{
    public class CommentLikedPush : PushNotificationMessage
    {
        [JsonProperty("articleId")]
        public Guid ArticleId { get; set; }

        [JsonProperty("newsHeader")]
        public string NewsHeader { get; set; }

        public CommentLikedPush()
        {
            Title = "Comment liked.";
            Body = "Someone liked your comment. Check it out!";
            NotificationType = (int) PushNotificationType.CommentLiked;
        }
    }
}
