// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Comment
{
    public class InCommentMentionedPush : BaseCommentPushNotificationMessage
    {
        public InCommentMentionedPush()
        {
            Title = "Mention in comment.";
            Body = "Someone mentioned you in comment. Check it out!";
            NotificationType = (int) PushNotificationType.InCommentMentioned;
        }
    }
}
