// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Comment
{
    public class CommentLikedPush : BaseCommentPushNotificationMessage
    {
        public CommentLikedPush()
        {
            Title = "Comment liked.";
            Body = "Someone liked your comment. Check it out!";
            NotificationType = (int) PushNotificationType.CommentLiked;
        }
    }
}
