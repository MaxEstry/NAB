#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("[iOS only] Waits and then sends the specified event when a purchase restoration completes.")]
    public class InAppPurchasing_OnRestoreCompleted : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when a purchase restoration completes.")]
        public FsmEvent sendEvent;

        public override void Reset()
        {
            eventTarget = null;
            sendEvent = null;
        }

        public override void OnEnter()
        {
            InAppPurchasing.RestoreCompleted += IAPManager_RestoreCompleted;
        }

        public override void OnExit()
        {
            InAppPurchasing.RestoreCompleted -= IAPManager_RestoreCompleted;
        }

        void IAPManager_RestoreCompleted()
        {
            Fsm.Event(eventTarget, sendEvent);
        }
    }
}
#endif

