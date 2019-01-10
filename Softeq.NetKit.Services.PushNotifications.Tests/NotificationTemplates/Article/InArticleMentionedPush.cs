// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Services.PushNotifications.Tests.NotificationTemplates.Article
{
    public class InArticleMentionedPush : BaseArticlePushNotificationMessage
    {
        public InArticleMentionedPush()
        {
            Title = "Mention in article.";
            Body = "Someone mentioned you in article. Check it out!";
            NotificationType = (int) PushNotificationType.InArticleMentioned;
        }
    }
}
