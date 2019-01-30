# Softeq.NetKit.Services.PushNotifications

Softeq.NetKit.Services.PushNotifications component simplifies the process of bringing support for Push notificaitons to your project. It's build around Azure Notification Hub and supports the following push notification platforms:
1. Android 
2. iOS

# Getting Started

## Install 
1. Check-out master branch from repository;
2. Add a reference to Softeq.NetKit.Services.PushNotifications into target project.

## Configure Azure Notification Hub

Add Notification Hub settings to your ```appsettings.json``` file:
```json
    "Notifications": {
        "Push": {
            "NotificationHub": {
                "ConnectionString": "CONN_STR",
                "HubName": "HUB_NAME"
            }
        }
    }
```

## Configure DI Container

Softeq.NetKit.Services.PushNotifications comes with Autofac Module to register its dependencies. 
If Autofac is used on your project, add ```PushNotificationsModule``` to the container:
```csharp
    builder.RegisterModule(new Softeq.NetKit.Services.PushNotifications.PushNotificationsModule());
```

If different IoC container is used or manual registration is required, the following registrations need to be performed:
1. Set up and register NotificationHub configuration ```AzureNotificationHubConfiguration``` in your DI container
```csharp
    builder.Register(context =>
            {
                var config = context.Resolve<IConfiguration>();
                return new AzureNotificationHubConfiguration(
                    config["Notifications:Push:NotificationHub:ConnectionString"],
                    config["Notifications:Push:NotificationHub:HubName"]);
            }).SingleInstance();
```

2. Register ```IPushNotificationSubscriber``` implementation
```csharp
    builder.RegisterType<AzureNotificationHubSender>().As<IPushNotificationSubscriber>();
```

3. Register ```IPushNotificationSender``` implementation
```csharp
    builder.RegisterType<AzureNotificationHubSubscriber>().As<IPushNotificationSender>();
```

## Develop

Softeq.NetKit.Services.PushNotifications comes with ```PushNotificationMessage``` that represents a simple push message with the following structure;
```csharp
    public class PushNotificationMessage
    {
        [JsonIgnore]
        public int NotificationType { get; set; }

        [JsonIgnore]
        public string Title { get; set; } = string.Empty;

        [JsonIgnore]
        public string Body { get; set; } = string.Empty;

        [JsonIgnore]
        public int Badge { get; set; } = 0;
        
        [JsonIgnore]
        public string Sound { get; set; } = "default";
        
        public virtual string GetData()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
```

If an additional properties are required to be sent with push, create custom push message class and inherit from ```PushNotificationMessage```
```csharp
    public class ArticleLikedPush : PushNotificationMessage
    {
        public ArticleLikedPush()
        {
            Title = "Article liked.";
            Body = "Someone liked your article. Check it out!";
            NotificationType = (int) PushNotificationType.ArticleLiked;
        }

        [JsonProperty("articleId")]
        public Guid ArticleId { get; set; }

        [JsonProperty("newsHeader")]
        public string NewsHeader { get; set; }
    }
```

## Use

Inject ```IPushNotificationSubscriber``` into your service to subscribe or unsubscribe user device from push notifications.

Inject ```IPushNotificationSender``` into your service to send push notifications to registered devices.

## About

This project is maintained by Softeq Development Corp.

We specialize in .NET core applications.

## Contributing

We welcome any contributions.

## License

The Query Utils project is available for free use, as described by the [LICENSE](/LICENSE) (MIT).
