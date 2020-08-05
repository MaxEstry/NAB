#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Returns the data of the specified product.")]
    public class InAppPurchasing_GetProductData : FsmStateAction
    {
        [Tooltip("Check to select from the list of registered products. " +
            "It is necessary to generate the IAP constants in Easy Mobile settings before using this option.")]
        public FsmBool selectFromProductList;

        [Tooltip("Select a product or enter the product name.")]
        public FsmString productName;

        [ActionSection("Result")]

        [Tooltip("The name of the selected product.")]
        [UIHint(UIHint.Variable)]
        public FsmString name;

        [Tooltip("The ID of the selected product.")]
        [UIHint(UIHint.Variable)]
        public FsmString id;

        [Tooltip("The type of the selected product")]
        [UIHint(UIHint.Variable)]
        [ObjectType(typeof(IAPProductType))]
        public FsmEnum type;

        [Tooltip("The price string of the selected product.")]
        [UIHint(UIHint.Variable)]
        public FsmString priceString;

        [Tooltip("The description of the selected product.")]
        [UIHint(UIHint.Variable)]
        public FsmString description;

        public override void Reset()
        {
            selectFromProductList = true;
            productName = null;
            name = null;
            id = null;
            type = null;
            priceString = null;
            description = null;
        }

        public override void OnEnter()
        {
            IAPProduct pd = InAppPurchasing.GetIAPProductByName(productName.Value);

            if (pd != null)
            {
                name.Value = pd.Name;
                id.Value = pd.Id;
                type.Value = pd.Type;
            }

            Finish();
        }
    }
}
#endif
