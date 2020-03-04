#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google Play. " +
        "Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt. " +
        "Use the 'On Restore Purchases Completed' or 'On Restore Purchases Failed' action to acknowledge when the restoration" +
        "completes or fails.")]
    public class InAppPurchasing_RestorePurchases : FsmStateAction
    {
        public override void OnEnter()
        {
            InAppPurchasing.RestorePurchases();
            Finish();
        }
    }
}
#endif

