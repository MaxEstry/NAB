#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Cancels the pending local notification with the specified ID.")]
    public class Notification_CancelPendingLocalNotification : FsmStateAction
    {
        [Tooltip("Identifier of the notification")]
        public FsmString id;

        public override void OnEnter()
        {
            Notifications.CancelPendingLocalNotification(id.Value);
            Finish();
        }

        public override void Reset()
        {
            id = null;
        }
    }
}
#endif
