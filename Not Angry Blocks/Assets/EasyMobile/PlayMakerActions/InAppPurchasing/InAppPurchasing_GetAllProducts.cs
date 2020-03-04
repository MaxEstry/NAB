#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Returns arrays of names, IDs and types of all in-app products. " +
        "Each product is referenced by the same index in all resulted arrays.")]
    public class InAppPurchasing_GetAllProducts : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The number of products found.")]
        [UIHint(UIHint.Variable)]
        public FsmInt productCount;

        [Tooltip("The names of all products.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray productNames;

        [Tooltip("The IDs of all products.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray productIds;

        [Tooltip("The types of all products.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(typeof(IAPProductType))]
        public FsmArray productTypes;

        [Tooltip("The price strings of all products.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray priceStrings;

        [Tooltip("The descriptions of all products.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray descriptions;

        public override void Reset()
        {
            productCount = null;
            productNames = null;
            productIds = null;
            productTypes = null;
            priceStrings = null;
            descriptions = null;
        }

        public override void OnEnter()
        {
            IAPProduct[] products = EM_Settings.InAppPurchasing.Products;

            productCount.Value = products.Length;
            productNames.Values = new object[productCount.Value];
            productIds.Values = new object[productCount.Value];
            productTypes.Values = new object[productCount.Value];
            priceStrings.Values = new object[productCount.Value];
            descriptions.Values = new object[productCount.Value];

            for (int i = 0; i < productCount.Value; i++)
            {
                productNames.Values[i] = (object)products[i].Name;
                productIds.Values[i] = (object)products[i].Id;
                productTypes.Values[i] = (object)products[i].Type;
                priceStrings.Values[i] = (object)products[i].Price;
                descriptions.Values[i] = (object)products[i].Description;
            }

            Finish();
        }
    }
}
#endif

