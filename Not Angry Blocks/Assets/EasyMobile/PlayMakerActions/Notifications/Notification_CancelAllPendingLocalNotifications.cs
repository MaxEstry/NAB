#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Cancels all pending local notifications.")]
    public class Notification_CancelAllPendingLocalNotifications : FsmStateAction
    {
        public override void OnEnter()
        {
            Notifications.CancelAllPendingLocalNotifications();
            Finish();
        }
    }
}
#endif
