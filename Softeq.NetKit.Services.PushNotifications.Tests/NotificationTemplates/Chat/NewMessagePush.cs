// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Models;

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Chat
{
    /// <summary>
    /// Chat notification
    /// </summary>
    public class NewMessagePush : PushNotificationMessage
    {
        public NewMessagePush()
        {
            Title = "New chat message.";
            Body = "New chat message. Check it out!";
            NotificationType = (int) PushNotificationType.ChatMessage;
        }

        [JsonProperty("channelId")]
        public Guid ChannelId { get; set; }
    }
}
