#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using System.Linq;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Show detailed information of opponents")]
    public class DemoUtils_TurnBased_ShowOpponentsInfo : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(DemoUtils_TurnBasedManager))]
        [Tooltip("Object contain current match data")]
        public FsmGameObject turnBaseManager;

        [ActionSection("Result")]

        [Tooltip("Returned string")]
        public FsmString alertString;

        [Tooltip("Event sent get opponents info successful ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if there is no current match ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Event sent if no Opponent. ")]
        public FsmEvent isNoOpponent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            turnBaseManager = null;
            alertString = null;
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
                var opponents = currentMatch.Participants.Where(p => p.ParticipantId != currentMatch.SelfParticipantId);
                if (opponents.Count() < 1)
                {
                    alertString.Value = "No one has joined your match yet. Auto-match players only appear after they joined the game.";
                    Fsm.Event(eventTarget, isNoOpponent);
                }

                alertString.Value = string.Join("\n\n", opponents.Select(p => manager.GetParticipantDisplayString(p)).ToArray());

                Fsm.Event(eventTarget, isSuccessEvent);
            }


        }
    }
}

#endif