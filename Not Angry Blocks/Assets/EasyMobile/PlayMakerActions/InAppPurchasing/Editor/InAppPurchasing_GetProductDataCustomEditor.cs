#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;
using System.Collections.Generic;
using EasyMobile.Editor;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(InAppPurchasing_GetProductData))]
    public class InAppPurchasing_GetProductDataCustomEditor : CustomActionEditor
    {
        InAppPurchasing_GetProductData _action;
        protected Dictionary<string, string> iapConstsDict;
        protected string[] iapConsts;

        public override void OnEnable()
        {
            _action = (InAppPurchasing_GetProductData)target;

            //Prepare a string array of the names of existing products.
            iapConstsDict = EM_EditorUtil.GetIAPConstants();
            iapConsts = new string[iapConstsDict.Count + 1];
            iapConsts[0] = EM_Constants.NoneSymbol;
            iapConstsDict.Keys.CopyTo(iapConsts, 1);
        }

        public override bool OnGUI()
        {
            EditField("selectFromProductList");

            bool changed = false;

            if (_action.selectFromProductList.Value)
                changed = EditSelectItemField("Product", _action.productName);
            else
                EditField("productName");

            // Results
            EditField("name");
            EditField("id");
            EditField("type");
            EditField("priceString");
            EditField("description");

            return changed || GUI.changed;
        }

        protected bool EditSelectItemField(string fieldDisplayName, FsmString field)
        {
            if (iapConsts.Length == 1)
            {
                EditorGUILayout.HelpBox("No product found. Please open EasyMobile settings to create some products and " +
                    "generate the IAPConstants class to select from the product list.", MessageType.Error);
            }

            EditorGUI.BeginChangeCheck();

            int currentIndex = 0;

            if (field != null)
                currentIndex = Mathf.Max(System.Array.IndexOf(iapConsts, EM_EditorUtil.GetKeyForValue(iapConstsDict, field.Value)), 0);
            else
                field = string.Empty;

            int newIndex = EditorGUILayout.Popup(fieldDisplayName, currentIndex, iapConsts);

            if (EditorGUI.EndChangeCheck())
            {
                // Position 0 is [None].
                if (newIndex == 0)
                {
                    field.Value = string.Empty;
                }
                else
                {
                    field.Value = iapConstsDict[iapConsts[newIndex]];
                }
            }

            return GUI.changed;
        }
    }
}
#endif