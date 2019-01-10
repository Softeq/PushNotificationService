// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Article
{
    public class ArticleLikedPush : BaseArticlePushNotificationMessage
    {
        public ArticleLikedPush()
        {
            Title = "Article liked.";
            Body = "Someone liked your article. Check it out!";
            NotificationType = (int) PushNotificationType.ArticleLiked;
        }
    }
}
