#if PLAYMAKER
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Waits until a remote/push notification is opened," +
        "then writes the notification content into the variable and sends the specified event. " +
        "You can then take appropriate actions like taking the user to app stores to download an update when it's available. " +
        "You should add this action to an object that is active early in your app, e.g. when the app starts.")]
    public class Notification_OnRemoteNotificationOpened : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when a push notification is opened.")]
        public FsmEvent notificationOpenedEvent;

        [Tooltip("The notification message.")]
        [UIHint(UIHint.Variable)]
        public FsmString message;

        [Tooltip("The ID of the button the user pressed, actionID will equal \"__DEFAULT__\" " +
            "when the notification itself was tapped when buttons were present.")]
        [UIHint(UIHint.Variable)]
        public FsmString actionId;

        [Tooltip("Was app in focus when the notification was opened. Normally you should take actions (e.g take user to your store) " +
            "only when this value is FALSE for not interrupting user experience.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isAppInFocus;

        [Tooltip("The number of items in the additional data of the notification.")]
        [UIHint(UIHint.Variable)]
        public FsmInt additionalDataItemCount;

        [Tooltip("The keys of the additional data items of the notification. " +
            "The associated value of each key has the same index in the 'Additional Data Values' array.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray additionalDataKeys;

        [Tooltip("The values of the additional data items of the notification. " +
            "The associated key of each value has the same index in the 'Additional Data Keys' array.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray additionalDataValues;

        public override void Reset()
        {
            eventTarget = null;
            notificationOpenedEvent = null;
            message = null;
            actionId = null;
            additionalDataItemCount = null;
            additionalDataKeys = null;
            additionalDataValues = null;
            isAppInFocus = null;
        }

        #pragma warning disable 0618
        public override void OnEnter()
        {
            Notifications.RemoteNotificationOpened += OnNotificationOpened;
        }

        public override void OnExit()
        {
            Notifications.RemoteNotificationOpened -= OnNotificationOpened;
        }
        #pragma warning restore 0618

        void OnNotificationOpened(RemoteNotification notif)
        {
            message.Value = notif.content.body;
            actionId.Value = notif.id;
            isAppInFocus.Value = notif.isAppInForeground;

            var data = notif.content.userInfo;
            if (data != null)
            {
                additionalDataItemCount.Value = data.Count;
                additionalDataKeys.Values = new object[data.Count];
                additionalDataValues.Values = new object[data.Count];

                int i = 0;

                foreach (KeyValuePair<string, object> pair in data)
                {
                    additionalDataKeys.Values[i] = (object)pair.Key;
                    additionalDataValues.Values[i] = (object)pair.Value.ToString();
                    i++;
                }
            }

            Fsm.Event(eventTarget, notificationOpenedEvent);
        }
    }
}

#endif
