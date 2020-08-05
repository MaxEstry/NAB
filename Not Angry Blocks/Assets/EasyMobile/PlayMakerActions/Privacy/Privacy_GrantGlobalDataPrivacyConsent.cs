#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Privacy")]
    [Tooltip("Grants the global data privacy consent.")]
    public class Privacy_GrantGlobalDataPrivacyConsent : FsmStateAction
    {
        public override void OnEnter()
        {
            Privacy.GrantGlobalDataPrivacyConsent();
            Finish();
        }
    }
}
#endif

