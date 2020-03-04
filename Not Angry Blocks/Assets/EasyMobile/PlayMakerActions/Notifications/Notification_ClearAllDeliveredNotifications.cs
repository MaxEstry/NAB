#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Removes all previously shown notifications of this app from the notification center or status bar.")]
    public class Notification_ClearAllDeliveredNotifications : FsmStateAction
    {
        public override void OnEnter()
        {
            Notifications.ClearAllDeliveredNotifications();
            Finish();
        }
    }
}
#endif
