#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("[iOS only] Waits and then sends the specified event when a purchase restoration fails.")]
    public class InAppPurchasing_OnRestoreFailed : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when a purchase restoration fails.")]
        public FsmEvent sendEvent;

        public override void Reset()
        {
            eventTarget = null;
            sendEvent = null;
        }

        public override void OnEnter()
        {
            InAppPurchasing.RestoreFailed += IAPManager_RestoreFailed;
        }

        public override void OnExit()
        {
            InAppPurchasing.RestoreFailed -= IAPManager_RestoreFailed;
        }

        void IAPManager_RestoreFailed()
        {
            Fsm.Event(eventTarget, sendEvent);
        }
    }
}
#endif

