// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Comment
{
    public class CommentRepliedPush : BaseCommentPushNotificationMessage
    {
        public CommentRepliedPush()
        {
            Title = "Comment replied.";
            Body = "Someone replied your comment. Check it out!";
            NotificationType = (int) PushNotificationType.CommentReplied;
        }
    }
}
