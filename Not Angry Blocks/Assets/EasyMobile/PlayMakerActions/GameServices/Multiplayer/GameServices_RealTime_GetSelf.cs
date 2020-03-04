#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Gets the participant that represents the current player.")]
    public class GameServices_RealTime_GetSelf : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The Unity Object containing the self Participant data")]
        public FsmObject participantObject;

        public override void Reset()
        {
            base.Reset();
            participantObject = null;
        }

        public override void OnEnter()
        {
            Participant participant = GameServices.RealTime.GetSelf();

            ParticipantObject temp = new ParticipantObject();

            temp.ParticipantData = participant;

            if (participant.Player != null)
            {
                temp.Image = participant.Player.image;
            }

            participantObject.Value = temp;

            Finish();
        }
    }
}

#endif