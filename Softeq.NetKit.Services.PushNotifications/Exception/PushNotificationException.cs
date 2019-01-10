// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Services.PushNotifications.Exception
{
    public class PushNotificationException : System.Exception
    {
        public PushNotificationException(string message)
            : base(message)
        {
        }

        public PushNotificationException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
