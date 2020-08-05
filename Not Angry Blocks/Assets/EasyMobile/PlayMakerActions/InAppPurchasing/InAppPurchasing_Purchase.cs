#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Purchases the product specified by its name or ID. Use the 'On Purchase Completed' or 'On Purchase Failed' " +
        "action to acknowledge when the purchase completes or fails.")]
    public class InAppPurchasing_Purchase : FsmStateAction
    {
        public enum ProductParam
        {
            Name,
            ID
        }

        [Tooltip("Whether to specify the product by its name or its ID.")]
        public ProductParam productParam;

        [Tooltip("The name of the product to purchase.")]
        public FsmString productName;

        [Tooltip("The ID of the product to purchase.")]
        public FsmString productId;

        public override void Reset()
        {
            productParam = ProductParam.Name;
            productName = null;
            productId = null;
        }

        public override void OnEnter()
        {
            if (productParam == ProductParam.Name)
                InAppPurchasing.Purchase(productName.Value);
            else
                InAppPurchasing.PurchaseWithId(productId.Value);

            Finish();
        }
    }
}
#endif

