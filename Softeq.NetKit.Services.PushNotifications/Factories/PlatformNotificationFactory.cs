// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.NetKit.Services.PushNotifications.Attributes;
using Softeq.NetKit.Services.PushNotifications.Exception;
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
            ValidateParameters(message);

            var properties = new Dictionary<string, string>
            {
                {"sound", message.Sound},
                {"badge", message.Badge.ToString()},
                {"type", message.NotificationType.ToString()},
                {"data", message.GetData()}
            };

            PopulateContent(message, properties);

            return properties;
        }

        private static void ValidateParameters(PushNotificationMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.BodyLocalizationKey) &&
                string.IsNullOrWhiteSpace(message.TitleLocalizationKey) &&
                string.IsNullOrWhiteSpace(message.Body) &&
                string.IsNullOrWhiteSpace(message.Title))
            {
                throw new ValidationException("Push notification message is missing the content.");
            }
        }

        private static void PopulateContent(PushNotificationMessage message, Dictionary<string, string> properties)
        {
            AddNativeParameters(message, properties);
            AddLocalizationKeyValues(message, properties);
        }

        private static void AddNativeParameters(PushNotificationMessage message, Dictionary<string, string> properties)
        {
            properties.Add("body", message.FormatBody());
            properties.Add("title", message.FormatTitle());
        }

        private static void AddLocalizationKeyValues(PushNotificationMessage message, Dictionary<string, string> properties)
        {
            properties.Add("body_loc_key", message.BodyLocalizationKey);
            properties.Add("title_loc_key", message.TitleLocalizationKey);

            var bodyArgValues = new List<MessageAttributeValue>();
            var titleArgValues = new List<MessageAttributeValue>();

            foreach (var prop in message.GetType().GetProperties())
            {
                var locAttributes = prop.GetCustomAttributes<LocalizationParameterAttribute>().ToList();
                if (locAttributes.Count > 0)
                {
                    var value = prop.GetValue(message).ToString();
                    var bodyAttribute = locAttributes.LastOrDefault(x => x.Target == LocalizationTarget.Body);
                    if (bodyAttribute != null)
                    {
                        bodyArgValues.Add(new MessageAttributeValue(value, bodyAttribute.Position));
                    }

                    var titleAttribute = locAttributes.LastOrDefault(x => x.Target == LocalizationTarget.Title);
                    if (titleAttribute != null)
                    {
                        titleArgValues.Add(new MessageAttributeValue(value, titleAttribute.Position));
                    }
                }
            }

            foreach (var arg in bodyArgValues.OrderBy(x => x.Position))
            {
                properties.Add($"body_loc_arg{arg.Position}", arg.Value);
            }

            foreach (var arg in titleArgValues.OrderBy(x => x.Position))
            {
                properties.Add($"title_loc_arg{arg.Position}", arg.Value);
            }
        }

        private readonly struct MessageAttributeValue
        {
            public MessageAttributeValue(string value, int position)
            {
                Value = value;
                Position = position;
            }

            public string Value { get; }
            public int Position { get; }
        }
    }
}
