#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Determines whether the product with the specified name is owned." +
        " A product is consider owned if it has a receipt. If receipt validation" +
        " is enabled, it is also required that this receipt passes the validation check." +
        " Note that this method is mostly useful with non-consumable products." +
        " Consumable products' receipts are not persisted between app restarts," +
        " therefore their ownership only pertains in the session they're purchased." +
        " In the case of subscription products, this method only checks if a product has been purchased before," +
        " it doesn't check if the subscription has been expired or canceled.")]
    public class InAppPurchasing_IsProductOwned : FsmStateAction
    {
        [Tooltip("The product name.")]
        [RequiredField]
        public FsmString productName;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if the product is owned, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isOwned;

        [Tooltip("Event sent if the product is owned.")]
        public FsmEvent isOwnedEvent;

        [Tooltip("Event sent if the product is not owned.")]
        public FsmEvent isNotOwnedEvent;

        public override void Reset()
        {
            productName = null;
            everyFrame = false;
            isOwned = null;
            isOwnedEvent = null;
            isNotOwnedEvent = null;
        }

        public override void OnEnter()
        {
            DoMyAction();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoMyAction();
        }

        void DoMyAction()
        {
            isOwned.Value = InAppPurchasing.IsProductOwned(productName.Value);

            if (isOwned.Value)
                Fsm.Event(isOwnedEvent);
            else
                Fsm.Event(isNotOwnedEvent);
        }
    }
}
#endif

