#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Show current match infomation")]
    public class DemoUtils_TurnBased_GetCurrentMatch : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(DemoUtils_TurnBasedManager))]
        [Tooltip("Object contain current match data")]
        public FsmGameObject turnBaseManager;

        [ActionSection("Result")]

        [Tooltip("Returned string")]
        public FsmString alertString;

        [Tooltip("Returned current match Id")]
        public FsmString currentMatchId;

        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [Tooltip("Next participant Id")]
        public FsmString nextParticipantId;

        [Tooltip("Check if selected participant left the match.")]
        public FsmBool selectedParticipantLeftMatch;

        [Tooltip("Is my turn")]
        public FsmBool isMyTurn;

        [Tooltip("Whether you can rematch")]
        public FsmBool canRematch;

        [Tooltip("Event sent if get the current match successful ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if get the current match not successful ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            turnBaseManager = null;
            alertString = null;
            currentMatchId = null;
            nextParticipantId = null;
            selectedParticipantLeftMatch = false;
            matchObject = null;
        }

        public override void OnEnter()
        {

            DemoUtils_TurnBasedManager manager = turnBaseManager.Value.GetComponent<DemoUtils_TurnBasedManager>();
            TurnBasedMatch currentMatch = manager.CurrentMatch;
            if (currentMatch == null)
            {
                alertString.Value = "Please create a match first.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }
            else
            {
                TurnBasedMatchObject temp = new TurnBasedMatchObject();

                temp.Match = currentMatch;

                matchObject.Value = temp;

                alertString.Value = manager.GetMatchDisplayString(currentMatch);

                currentMatchId.Value = currentMatch.MatchId;

                if (manager.SelectedParticipant != null)
                    nextParticipantId.Value = manager.SelectedParticipant.ParticipantId;
                else
                    nextParticipantId.Value = null;
                selectedParticipantLeftMatch.Value = manager.SelectedParticipantLeftMatch;
                isMyTurn.Value = manager.IsMyTurn;
                canRematch.Value = manager.CanRematch;
                Fsm.Event(eventTarget, isSuccessEvent);
            }
        }
        public override void OnExit()
        {

        }

    }
}

#endif