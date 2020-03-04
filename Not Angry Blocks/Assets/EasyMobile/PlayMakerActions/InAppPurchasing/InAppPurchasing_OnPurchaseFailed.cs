#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Waits and then sends the specified event when an in-app purchase fails.")]
    public class InAppPurchasing_OnPurchaseFailed : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The name of the purchased product.")]
        [UIHint(UIHint.Variable)]
        public FsmString productName;

        [Tooltip("The ID of the purchased product.")]
        [UIHint(UIHint.Variable)]
        public FsmString productId;

        [Tooltip("The type of the purchased product.")]
        [ObjectType(typeof(IAPProductType))]
        [UIHint(UIHint.Variable)]
        public FsmEnum productType;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when an in-app purchase fails.")]
        public FsmEvent sendEvent;

        public override void Reset()
        {
            eventTarget = null;
            sendEvent = null;
            productName = null;
            productId = null;
            productType = null;
        }

        public override void OnEnter()
        {
            InAppPurchasing.PurchaseFailed += IAPManager_PurchaseFailed;
        }

        public override void OnExit()
        {
            InAppPurchasing.PurchaseFailed -= IAPManager_PurchaseFailed;
        }

        void IAPManager_PurchaseFailed(IAPProduct p)
        {
            productName.Value = p.Name;
            productId.Value = p.Id;
            productType.Value = p.Type;
            Fsm.Event(eventTarget, sendEvent);
        }
    }
}
#endif

