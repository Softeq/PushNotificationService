// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Article
{
    public class ArticleLikedPush : PushNotificationMessage
    {
        [JsonProperty("articleId")]
        public Guid ArticleId { get; set; }

        [JsonProperty("newsHeader")]
        public string NewsHeader { get; set; }

        public ArticleLikedPush()
        {
            Title = "Article liked.";
            Body = "Someone liked your article. Check it out!";
            NotificationType = (int) PushNotificationType.ArticleLiked;
        }
    }
}
