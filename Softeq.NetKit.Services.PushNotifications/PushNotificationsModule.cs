// Developed by Softeq Development Corporation
// http://www.softeq.com

using Autofac;
using Microsoft.Extensions.Configuration;
using Softeq.NetKit.Services.PushNotifications.Abstractions;
using Softeq.NetKit.Services.PushNotifications.Client;

namespace Softeq.NetKit.Services.PushNotifications
{
    public class PushNotificationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var config = context.Resolve<IConfiguration>();
                return new AzureNotificationHubConfiguration(
                    config["Notifications:Push:NotificationHub:ConnectionString"],
                    config["Notifications:Push:NotificationHub:HubName"]);
            }).SingleInstance();
            builder.RegisterType<AzureNotificationHubSender>().As<IPushNotificationSubscriber>();
            builder.RegisterType<AzureNotificationHubSubscriber>().As<IPushNotificationSender>();
        }
    }
}
