﻿// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Article
{
    public abstract class BaseArticlePushNotificationMessage : PushNotificationMessage
    {
        [JsonProperty("articleId")]
        public Guid ArticleId { get; set; }

        [JsonProperty("newsHeader")]
        public string NewsHeader { get; set; }
    }
}