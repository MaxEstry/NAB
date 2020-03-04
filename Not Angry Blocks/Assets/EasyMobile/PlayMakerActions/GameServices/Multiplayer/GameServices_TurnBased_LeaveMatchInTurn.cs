#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Leaves the match (during turn). Call this to leave the match when it's your turn.")]
    public class GameServices_TurnBased_LeaveMatchInTurn : FsmStateAction
    {
        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [Tooltip("Next Participant Id.")]
        public FsmString nextParticipantId;

        [ActionSection("Result")]

        [Tooltip("True if success.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Event sent if leave match in turn successful. ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if leave match in turn not successful. ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            matchObject = null;
            nextParticipantId = null;
        }

        public override void OnEnter()
        {
            if (string.IsNullOrEmpty(nextParticipantId.Value))
                nextParticipantId.Value = null;

            TurnBasedMatchObject temp = (TurnBasedMatchObject)matchObject.Value;

            TurnBasedMatch match = temp.Match;

            GameServices.TurnBased.LeaveMatchInTurn(match, nextParticipantId.Value, LeaveMatchInTurnCallBack);
        }       

        void LeaveMatchInTurnCallBack(bool isLeaveMatch)
        {
            if (isLeaveMatch)
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