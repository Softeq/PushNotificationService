// Developed by Softeq Development Corporation
// http://www.softeq.com

using Newtonsoft.Json;
using Softeq.NetKit.Services.PushNotifications.Attributes;
using Softeq.NetKit.Services.PushNotifications.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Softeq.NetKit.Services.PushNotifications.Factories
{
    internal static class PlatformNotificationFactory
    {
        public static IDictionary<string, string> CreateTemplateProperties(PushNotificationMessage message)
        {
            var properties = new Dictionary<string, string>
            {
                {"body", message.Body},
                {"title", message.Title},
                {"sound", message.Sound},
                {"badge", message.Badge.ToString()},
                {"type", message.NotificationType.ToString()},
                {"data", message.GetData()}
            };

            AddLocalizationKeyValues(message, properties);

            return properties;
        }

        private static void AddLocalizationKeyValues(PushNotificationMessage message, Dictionary<string, string> properties)
        {
            properties.Add("body_loc_key", message.BodyLocalizationKey);
            properties.Add("title_loc_key", message.TitleLocalizationKey);

            //TODO: investigate localization arguments propagation
            return;

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
