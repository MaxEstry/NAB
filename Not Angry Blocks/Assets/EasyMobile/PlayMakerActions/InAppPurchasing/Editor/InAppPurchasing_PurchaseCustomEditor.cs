#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMakerEditor;
using System;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(InAppPurchasing_Purchase))]
    public class InAppPurchasing_PurchaseCustomEditor : CustomActionEditor
    {
        InAppPurchasing_Purchase _action;

        public override bool OnGUI()
        {
            _action = (InAppPurchasing_Purchase)target;

            EditField("productParam");

            switch (_action.productParam)
            {
                case InAppPurchasing_Purchase.ProductParam.ID:
                    EditField("productId");
                    break;
                case InAppPurchasing_Purchase.ProductParam.Name:
                    EditField("productName");
                    break;
            }

            return GUI.changed;
        }
    }
}
#endif


