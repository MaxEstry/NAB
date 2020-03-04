#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using System.Collections.Generic;

namespace EasyMobile.PlayMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Schedules a local notification to be posted after the specified delay time,"
       + "Note that the scheduled notification persists even if the device reboots, and it"
       + "will be fired immediately after the reboot if date is past.")]
    public class Notification_ScheduleLocalNotificationRepeat : FsmStateAction
    {
        [Tooltip("The ID of the scheduled notification.")]
        [UIHint(UIHint.Variable)]
        public FsmString id;

        [Tooltip("repeat delay hourse")]
        public FsmInt repeatDelayHours;

        [Tooltip("repeat delay minutes")]
        public FsmInt repeatDelayMinutes;

        [Tooltip("repeat delay seconds")]
        public FsmInt repeatDelaySeconds;

        [Header("Notificontent")]
        [Tooltip("The notification title.")]
        public FsmString title;

        [Tooltip("[iOS only] The notification subtitle.")]
        public FsmString subtitle;

        [Tooltip("The message displayed in the notification alert.")]
        public FsmString body;

        [Tooltip("Type is NotificationRepeat")]
        [ArrayEditor(typeof(NotificationRepeat))]
        public FsmEnum repeat = NotificationRepeat.EveryMinute;

        [Tooltip("[iOS only] The number to display as the app’s icon badge."
            + "When the number in this property is 0 or smaller, the system does not display a badge."
            + " When the number is greater than 0, the system displays the badge with the specified number.")]
        public FsmInt badge;

        [Tooltip("A dictionary to attach custom data to the notification. This is optional.")]
        [CompoundArray("UserInfo", "keyUserInfo", "valueUserInfo")]
        public FsmString[] keyUserInfo;
        public FsmString[] valueUserInfo;

        [Tooltip("The identifier of the category this notification belongs to."
            + "If no category is specified, the default one will be used.")]
        public FsmString categoryId;

        [Tooltip("[Android only] The small icon displayed on the notification."
            + "Give it a value only if you want to use a custom icon rather than the default one.")]
        public FsmString smallIcon = NotificationContent.DEFAULT_ANDROID_SMALL_ICON;

        [Tooltip("[Android only] The small icon displayed on the notification."
            + "Give it a value only if you want to use a custom icon rather than the default one.")]
        public FsmString largeIcon = NotificationContent.DEFAULT_ANDROID_LARGE_ICON;

        public override void Reset()
        {
            id = null;
            repeatDelayHours = 0;
            repeatDelayMinutes = 0;
            repeatDelaySeconds = 0;
            title = null;
            subtitle = null;
            body = null;
            badge = null;
            keyUserInfo = null;
            valueUserInfo = null;
            categoryId = null;
            smallIcon = NotificationContent.DEFAULT_ANDROID_SMALL_ICON; ;
            largeIcon = NotificationContent.DEFAULT_ANDROID_LARGE_ICON;
            repeat = NotificationRepeat.EveryMinute;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            DoMyAction();
        }

        void DoMyAction()
        {
            if (Notifications.IsInitialized())
            {
                TimeSpan spanDelay = new TimeSpan(repeatDelayHours.Value, repeatDelayMinutes.Value, repeatDelaySeconds.Value);
                NotificationContent content = new NotificationContent();
                content.title = title.Value;
                content.subtitle = subtitle.Value;
                content.body = body.Value;
                content.badge = badge.Value;
                content.categoryId = categoryId.Value;
                content.smallIcon = smallIcon.Value;
                content.largeIcon = largeIcon.Value;

                Dictionary<string, object> userInfo = new Dictionary<string, object>();
                int number;
                if (valueUserInfo.Length < keyUserInfo.Length)
                    number = valueUserInfo.Length;
                else
                    number = keyUserInfo.Length;
                for (int i = 0; i < number; i++)
                    userInfo.Add(keyUserInfo[i].Value, valueUserInfo[i].Value);
                content.userInfo = userInfo;


                id = Notifications.ScheduleLocalNotification(spanDelay, content, (NotificationRepeat)repeat.Value);
                Finish();
            }
        }
    }
}
#endif
