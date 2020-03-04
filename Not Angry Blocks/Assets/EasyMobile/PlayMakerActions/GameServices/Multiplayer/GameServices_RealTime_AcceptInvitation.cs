#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Accepts an invitation.")]
    public class GameServices_RealTime_AcceptInvitation : FsmStateAction
    {
        [Tooltip("The Unity Object containing the invitation data")]
        public FsmObject invitationObject;
        
        [Tooltip("Event Listener")]
        public FsmGameObject listener;

        public override void Reset()
        {
            base.Reset();
            invitationObject = null;
        }

        public override void OnEnter()
        {
            MultiplayerInvitationObject temp = (MultiplayerInvitationObject)invitationObject.Value;
            Invitation invitation = temp.Invitation;
            GameServices.RealTime.AcceptInvitation((Invitation)invitation, true,listener.Value.GetComponent<IRealTimeMultiplayerListener>());
            Finish();
        }
               
    }
}

#endif