#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Revokes the module-level data privacy consent of the Notifications module.")]
    public class Notification_RevokeModuleDataPrivacyConsent : FsmStateAction
    {
        public override void OnEnter()
        {
            Notifications.RevokeDataPrivacyConsent();
            Finish();
        }
    }
}
#endif

