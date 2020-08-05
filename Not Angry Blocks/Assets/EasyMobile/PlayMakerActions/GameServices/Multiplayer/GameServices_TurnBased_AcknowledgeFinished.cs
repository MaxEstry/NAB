#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Acknowledges that a match was finished. " +
        "Call this on a finished match that you have just shown to the user, " +
        "to acknowledge that the user has seen the results of the finished match. " +
        "This will remove the match from the user's inbox.")]
    public class GameServices_TurnBased_AcknowledgeFinished : FsmStateAction
    {
        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [ActionSection("Result")]

        [Tooltip("True if success.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Event sent if Acknowledge match successful. ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if Acknowledge match not successful. ")]
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

            GameServices.TurnBased.AcknowledgeFinished(match, AcknowledgeFinishedCallback);
        }
       
        void AcknowledgeFinishedCallback(bool isSuccessful)
        {
            if (isSuccessful)
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