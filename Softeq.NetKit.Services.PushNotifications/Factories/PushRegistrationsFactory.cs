﻿// Developed by Softeq Development Corporation
// http://www.softeq.com

using Microsoft.Azure.NotificationHubs;
using Softeq.NetKit.Services.PushNotifications.Models;
using System;
using System.Collections.Generic;

namespace Softeq.NetKit.Services.PushNotifications.Factories
{
    internal static class PushRegistrationsFactory
    {
        private const string iOSTemplate = "{\"aps\":{\"alert\":{\"title-loc-key\":\"$(title_loc_key)\", \"title-loc-args\":[\"$(title_loc_arg1)\", \"$(title_loc_arg2)\", \"$(title_loc_arg3)\"], \"loc-key\":\"$(body_loc_key)\", \"loc-args\":[\"$(body_loc_arg1)\",\"$(body_loc_arg2)\",\"$(body_loc_arg3)\"], \"title\":\"$(title)\", \"body\":\"$(body)\"}, \"sound\":\"$(sound)\", \"badge\":\"#(badge)\", \"type\":\"#(type)\"}, \"data\":\"$(data)\"}";
        private const string androidTemplate = "{\"notification\":{\"title_loc_key\":\"$(title_loc_key)\", \"title_loc_args\":[\"$(title_loc_arg1)\", \"$(title_loc_arg2)\", \"$(title_loc_arg3)\"], \"body_loc_key\":\"$(body_loc_key)\", \"body_loc_args\":[\"$(body_loc_arg1)\",\"$(body_loc_arg2)\",\"$(body_loc_arg3)\"], \"body\":\"$(body)\", \"title\":\"$(title)\", \"sound\":\"$(sound)\"}, \"data\":{\"type\":\"#(type)\", \"payload\":\"$(data)\"}}";

        public static RegistrationDescription CreateTemplateRegistration(PushSubscriptionRequest subscription)
        {
            switch (subscription.Platform)
            {
                case PushPlatformEnum.iOS:
                    return new AppleTemplateRegistrationDescription(subscription.DeviceHandle, iOSTemplate, new HashSet<string>(subscription.Tags));
                case PushPlatformEnum.Android:
                    return new FcmTemplateRegistrationDescription(subscription.DeviceHandle, androidTemplate, new HashSet<string>(subscription.Tags));
                default:
                    throw new NotSupportedException($"Unsupported platform '{subscription.Platform}'");
            }
        }
    }
}
