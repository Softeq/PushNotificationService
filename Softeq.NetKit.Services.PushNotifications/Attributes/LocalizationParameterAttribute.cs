// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Services.PushNotifications.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LocalizationParameterAttribute : Attribute
    {
        public LocalizationTarget Target { get; }

        public LocalizationParameterAttribute(LocalizationTarget target)
        {
            Target = target;
        }
    }

    public enum LocalizationTarget
    {
        Title,
        Body
    }
}
