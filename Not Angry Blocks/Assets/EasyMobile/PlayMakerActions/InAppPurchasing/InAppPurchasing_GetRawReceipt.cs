#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

#if EM_UIAP
using UnityEngine.Purchasing;
#endif

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Gets the receipt of the product if it is owned.")]
    public class InAppPurchasing_GetRawReceipt : FsmStateAction
    {
        [Tooltip("The name of the product.")]
        [RequiredField]
        public FsmString productName;

        [ActionSection("Result")]

        [Tooltip("The receipt of the product if owned, otherwise null.")]
        [UIHint(UIHint.Variable)]
        public FsmString receipt;

        public override void Reset()
        {
            productName = null;
            receipt = null;
        }

        public override void OnEnter()
        {
            #if EM_UIAP
            Product pd = InAppPurchasing.GetProduct(productName.Value);

            if (pd != null)
            {
                receipt.Value = pd.receipt;
            }
            #endif

            Finish();
        }
    }
}
#endif

