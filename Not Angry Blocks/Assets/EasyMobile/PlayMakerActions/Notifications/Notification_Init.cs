#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Initializes the push notification service. Only use this action if you disabled the Automatic initialization" +
        "feature of the Notification module.")]
    public class Notification_Init : FsmStateAction
    {

        public override void OnEnter()
        {
            Notifications.Init();
            Finish();
        }
    }
}

#endif
