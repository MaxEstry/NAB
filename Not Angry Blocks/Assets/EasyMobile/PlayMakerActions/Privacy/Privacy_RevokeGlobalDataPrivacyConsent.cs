#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Privacy")]
    [Tooltip("Revokes the global data privacy consent.")]
    public class Privacy_RevokeGlobalDataPrivacyConsent : FsmStateAction
    {
        public override void OnEnter()
        {
            Privacy.RevokeGlobalDataPrivacyConsent();
            Finish();
        }
    }
}
#endif

