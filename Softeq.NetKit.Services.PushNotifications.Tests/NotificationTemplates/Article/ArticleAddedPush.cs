// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Article
{
    public class ArticleAddedPush : BaseArticlePushNotificationMessage
    {
        public ArticleAddedPush()
        {
            Title = "Article added.";
            Body = "New article added. Check it out!";
            NotificationType = (int) PushNotificationType.NewsPosted;
        }
    }
}
