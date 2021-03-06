﻿// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.Messages
{
    public class CommentLikedPush : PushNotificationMessage
    {
        [JsonProperty("commentId")]
        public Guid CommentId { get; set; }

        public CommentLikedPush()
        {
            Title = "Comment liked.";
            Body = "Someone liked your comment. Check it out!";
            NotificationType = (int) PushNotificationType.CommentLiked;
        }
    }
}
