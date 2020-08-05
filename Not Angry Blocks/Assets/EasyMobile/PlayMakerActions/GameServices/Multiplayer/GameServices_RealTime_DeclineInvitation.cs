#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Declines the invitation.")]
    public class GameServices_RealTime_DeclineInvitation : FsmStateAction
    {
        [Tooltip("The Unity Object containing the invitation data")]
        public FsmObject invitationObject;

        public override void Reset()
        {
            base.Reset();
            invitationObject = null;
        }

        public override void OnEnter()
        {
            MultiplayerInvitationObject temp = (MultiplayerInvitationObject)invitationObject.Value;
            Invitation invitation = temp.Invitation;
            GameServices.RealTime.DeclineInvitation(invitation);
            Finish();
        }
               
    }
}

#endif