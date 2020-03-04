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
    [Tooltip("Gets information of a participant")]
    public class GameServices_Multiplayer_GetParticipantInfo : FsmStateAction
    {
        [Tooltip("The Unity Object containing the participant data")]
        public FsmObject participantObject;

        [ActionSection("Result")]        

        [Tooltip("Status of the participant that represents the current player.")]
        [ObjectType(typeof(Participant.ParticipantStatus))]
        public FsmEnum status;

        [Tooltip("The display name of the participant that represents the current player.")]
        public FsmString displayName;

        [Tooltip("The participant ID of the participant that represents the current player.")]
        public FsmString participantId;

        [Tooltip("player image of the participant that represents that current player.")]
        public FsmTexture image;

        [Tooltip("Returns whether the participant is connected to the real time room.")]
        public FsmBool isConnectedToRoom;

        public override void Reset()
        {
            base.Reset();
            status = Participant.ParticipantStatus.Unknown;
            displayName = null;
            participantId = null;
            image = null;
            isConnectedToRoom = false;
            participantObject = null;
        }

        public override void OnEnter()
        {
            ParticipantObject temp = (ParticipantObject)participantObject.Value;

            Participant participant = temp.ParticipantData;
            
            status.Value = participant.Status;
            displayName.Value = participant.DisplayName; ;
            participantId.Value = participant.ParticipantId;
            if (participant.Player != null)
            {
                image.Value = participant.Player.image;
            }
            else
                image = null;
            isConnectedToRoom = participant.IsConnectedToRoom;

            Finish();
        }  
    }
}

#endif