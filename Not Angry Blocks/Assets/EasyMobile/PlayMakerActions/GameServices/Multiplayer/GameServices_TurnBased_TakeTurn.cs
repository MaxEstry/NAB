#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using System;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Takes a turn. Before you call this method, make sure that" +
        " it is actually the player's turn in the match, otherwise this call will fail.")]
    public class GameServices_TurnBased_TakeTurn : FsmStateAction
    {
        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [Tooltip("Match data. The data to send in form of a base64 string.")]
        [UIHint(UIHint.Variable)]
        public FsmString data;

        [Tooltip("Next Participant Id.")]
        public FsmString nextParticipantId;

        [ActionSection("Result")]

        [Tooltip("True if success.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Event sent if take turn successful. ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if take turn not successful. ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            data = null;
            nextParticipantId = null;
            isSuccess = false;
            matchObject = null;
        }

        public override void OnEnter()
        {
            if (string.IsNullOrEmpty(nextParticipantId.Value))
                nextParticipantId.Value = null;

            TurnBasedMatchObject temp = (TurnBasedMatchObject)matchObject.Value;

            TurnBasedMatch match = temp.Match;

            byte[] binaryData = Convert.FromBase64String(data.Value);

            GameServices.TurnBased.TakeTurn(match, binaryData, nextParticipantId.Value, TakeTurnCallBack);  


        }       

        void TakeTurnCallBack(bool isTurnSuccess)
        {
            if (isTurnSuccess)
            {
                isSuccess.Value = true;                
                Fsm.Event(eventTarget, isSuccessEvent);
            }
            else
            {
                isSuccess.Value = false;               
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }        
        }
    }
}

#endif