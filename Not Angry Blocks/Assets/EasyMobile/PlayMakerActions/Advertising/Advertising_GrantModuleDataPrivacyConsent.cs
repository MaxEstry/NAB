#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Grants the module-level data privacy consent of the Advertising module.")]
    public class Advertising_GrantModuleDataPrivacyConsent : FsmStateAction
    {
        public override void OnEnter()
        {
            Advertising.GrantDataPrivacyConsent();
            Finish();
        }
    }
}
#endif

