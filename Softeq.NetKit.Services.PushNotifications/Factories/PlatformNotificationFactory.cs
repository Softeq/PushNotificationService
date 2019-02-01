// Developed by Softeq Development Corporation
// http://www.softeq.com

using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Attributes;
using Softeq.NetKit.Services.PushNotifications.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Softeq.NetKit.Services.PushNotifications.Exception;

namespace Softeq.NetKit.Services.PushNotifications.Factories
{
    internal static class PlatformNotificationFactory
    {
        public static IDictionary<string, string> CreateTemplateProperties(PushNotificationMessage message)
        {
            var properties = new Dictionary<string, string>
            {
                {"sound", message.Sound},
                {"badge", message.Badge.ToString()},
                {"type", message.NotificationType.ToString()},
                {"data", message.GetData()}
            };

            var notificationType = GetNotificationMessageType(message);

            switch (notificationType)
            {
                case NotificationType.Native:
                {
                    AddNativeParameters(message, properties);
                    break;
                }
                case NotificationType.Localized:
                {
                    AddLocalizationKeyValues(message, properties);
                    break;
                }
            }

            return properties;
        }

        private static NotificationType GetNotificationMessageType(PushNotificationMessage message)
        {
            if (!string.IsNullOrWhiteSpace(message.BodyLocalizationKey) &&
                !string.IsNullOrWhiteSpace(message.TitleLocalizationKey) &&
                !string.IsNullOrWhiteSpace(message.Body) &&
                !string.IsNullOrWhiteSpace(message.Title)
                )
            {
                throw new ValidationException("Push notification has mixed type.");
            }

            if (!string.IsNullOrWhiteSpace(message.Body) &&
                !string.IsNullOrWhiteSpace(message.Title))
            {
                return NotificationType.Native;
            }

            if (!string.IsNullOrWhiteSpace(message.BodyLocalizationKey) &&
                !string.IsNullOrWhiteSpace(message.TitleLocalizationKey))
            {
                return NotificationType.Localized;
            }

            throw new ValidationException("Push notification type is not recognized.");
        }

        private static void AddNativeParameters(PushNotificationMessage message, Dictionary<string, string> properties)
        {
            properties.Add("body", message.Body);
            properties.Add("title", message.Title);
        }

        private static void AddLocalizationKeyValues(PushNotificationMessage message, Dictionary<string, string> properties)
        {
            properties.Add("body_loc_key", message.BodyLocalizationKey);
            properties.Add("title_loc_key", message.TitleLocalizationKey);

            var bodyArgValues = new List<string>();
            var titleArgValues = new List<string>();

            foreach (var prop in message.GetType().GetProperties())
            {
                var locAttributes = prop.GetCustomAttributes<LocalizationParameterAttribute>().ToList();
                if (locAttributes.Any())
                {
                    var value = prop.GetValue(message).ToString();
                    var bodyAttribute = locAttributes.FirstOrDefault(x => x.Target == LocalizationTarget.Body);
                    if (bodyAttribute != null)
                    {
                        bodyArgValues.Add(value);
                    }

                    var titleAttribute = locAttributes.FirstOrDefault(x => x.Target == LocalizationTarget.Title);
                    if (titleAttribute != null)
                    {
                        titleArgValues.Add(value);
                    }
                }
            }
            
            var bodyArgsContent = JsonConvert.SerializeObject(bodyArgValues);
            var titleArgsContent = JsonConvert.SerializeObject(titleArgValues);

            properties.Add("body_loc_args", bodyArgsContent);
            properties.Add("title_loc_args", titleArgsContent);
        }
    }
}
