#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Creates a real-time game starting with the inbox screen.")]
    public class GameServices_RealTime_ShowInvitationsUI : FsmStateAction
    {
        [Tooltip("Event Listener")]
        public FsmGameObject listener;

        public override void Reset()
        {
            base.Reset();          
        }

        public override void OnEnter()
        {
            GameServices.RealTime.ShowInvitationsUI(listener.Value.GetComponent<IRealTimeMultiplayerListener>());
            Finish();
        }
               
    }
}

#endif