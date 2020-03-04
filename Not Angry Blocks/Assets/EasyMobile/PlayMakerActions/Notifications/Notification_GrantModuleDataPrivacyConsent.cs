#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Grants the module-level data privacy consent of the Notifications module.")]
    public class Notification_GrantModuleDataPrivacyConsent : FsmStateAction
    {
        public override void OnEnter()
        {
            Notifications.GrantDataPrivacyConsent();
            Finish();
        }
    }
}
#endif

