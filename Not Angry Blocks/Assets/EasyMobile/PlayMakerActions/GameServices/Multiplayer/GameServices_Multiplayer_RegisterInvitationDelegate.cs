#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using System.Text;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Creates a quick match with match request")]
    public class GameServices_Multiplayer_RegisterInvitationDelegate : FsmStateAction
    {  
        [ActionSection("Result")]

        [Tooltip("The Unity Object containing the invitation data")]
        public FsmObject invitationObject;

        [Tooltip("If this is true, then the game should immediately accept the invitation and go to the game screen " +
            "without prompting the user. If false, you should prompt the user before accepting the invitation. " +
            "As an example,  when a user taps on the \"Accept\" button on a notification in Android, it is clear that " +
            "they want to accept that invitation right away, so the plugin calls this delegate with shouldAutoAccept " +
            "= true. However, if we receive an incoming invitation that the player hasn't specifically indicated " +
            "they wish to accept (for example, we received one in the background from the server), this delegate will be" +
            " called with shouldAutoAccept=false to indicate that you should confirm with the user to see if they wish to" +
            " accept or decline the invitation.")]
        public FsmBool shouldAutoAccept;

        [Tooltip("Event sent when receiving a Turn-Based or Real-Time game invitation.")]
        public FsmEvent OnInvitationReceived;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;      

        public override void Reset()
        {
            base.Reset();
            invitationObject = null;
        }

        public override void OnEnter()
        {
            GameServices.RegisterInvitationDelegate(InternalOnInvitationReceived);
        }

        protected void InternalOnInvitationReceived(Invitation invitation, bool autoAccept)
        {
           
            if (invitation == null)
            {
                NativeUI.Alert("Error", "Received a null invitation!!!");
                return;
            }
            
            MultiplayerInvitationObject temp = new MultiplayerInvitationObject();

            temp.Invitation = invitation;
            
            invitationObject.Value = temp;

            shouldAutoAccept = autoAccept;

            Fsm.Event(eventTarget, OnInvitationReceived);
        }    

        public override void OnExit()
        {

        }      
        
    }
}

#endif