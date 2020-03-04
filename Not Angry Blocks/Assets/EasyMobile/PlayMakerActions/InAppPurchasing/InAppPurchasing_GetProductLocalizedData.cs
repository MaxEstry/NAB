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
    [Tooltip("Gets the product localized data provided by the stores.")]
    public class InAppPurchasing_GetProductLocalizedData : FsmStateAction
    {
        [Tooltip("The name of the product.")]
        [RequiredField]
        public FsmString productName;

        [ActionSection("Result")]

        [Tooltip("The localized title.")]
        [UIHint(UIHint.Variable)]
        public FsmString localizedTitle;

        [Tooltip("The localized product description.")]
        [UIHint(UIHint.Variable)]
        public FsmString localizedDescription;

        [Tooltip("The ISO currency code associated with the price.")]
        [UIHint(UIHint.Variable)]
        public FsmString isoCurrencyCode;

        [Tooltip("The localized price value.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat localizedPrice;

        [Tooltip("The localized price string.")]
        [UIHint(UIHint.Variable)]
        public FsmString localizedPriceString;

        public override void Reset()
        {
            productName = null;
            localizedTitle = null;
            localizedDescription = null;
            isoCurrencyCode = null;
            localizedPrice = null;
            localizedPriceString = null;
        }

        public override void OnEnter()
        {
            #if EM_UIAP
            ProductMetadata data = InAppPurchasing.GetProductLocalizedData(productName.Value);

            if (data != null)
            {
                localizedTitle.Value = data.localizedTitle;
                localizedDescription.Value = data.localizedDescription;
                isoCurrencyCode.Value = data.isoCurrencyCode;
                localizedPrice.Value = (float)data.localizedPrice;
                localizedPriceString.Value = data.localizedPriceString;
            }
            #endif

            Finish();
        }
    }
}
#endif

