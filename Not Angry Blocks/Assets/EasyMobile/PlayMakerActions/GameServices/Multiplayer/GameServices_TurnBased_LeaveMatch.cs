#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Leaves the match (not during turn). Call this to leave the match when it is not your turn.")]
    public class GameServices_TurnBased_LeaveMatch : FsmStateAction
    {
        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [ActionSection("Result")]

        [Tooltip("True if success.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Event sent if leave match successful. ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if leave match not successful. ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            matchObject = null;
        }

        public override void OnEnter()
        {
            TurnBasedMatchObject temp = (TurnBasedMatchObject)matchObject.Value;

            TurnBasedMatch match = temp.Match;

            GameServices.TurnBased.LeaveMatch(match, LeaveMatchCallback);
        }
        
        void LeaveMatchCallback(bool isLeaveMatch)
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