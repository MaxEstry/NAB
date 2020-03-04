#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Create a quick match with match request")]
    public class DemoUtils_TurnBased_CreateMatchOutcome : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(DemoUtils_TurnBasedManager))]
        [Tooltip("Object contain current match data")]
        public FsmGameObject turnBaseManager;

        [ActionSection("Result")]

        [Tooltip("Participant IDs.")]
        [ArrayEditor(VariableType.String)]
        public FsmArray participantIds;

        [Tooltip("Participant results.")]
        [ArrayEditor(typeof(MatchOutcome.ParticipantResult))]
        public FsmArray results;

        [Tooltip("Match Data. The data to send in form of a base64 string.")]
        public FsmString matchData;

        [Tooltip("Returned string")]
        public FsmString alertString;

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
            participantIds = null;
            results = null;
            matchData = null;
            alertString = null;
        }

        public override void OnEnter()
        {

            DemoUtils_TurnBasedManager manager = turnBaseManager.Value.GetComponent<DemoUtils_TurnBasedManager>();
            TurnBasedMatch currentMatch = manager.CurrentMatch;

            if (!manager.IsMyTurn)
            {
                alertString.Value = "Not your turn.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }

            if (currentMatch == null)
            {
                alertString.Value = "Please create a match first.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }

            if (manager.CurrentMatchData == null)
            {
                alertString.Value = "Couldn't find any match data.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }

            int participantLength = manager.CurrentOpponents.Length;
            participantIds.Resize(participantLength);
            results.Resize(participantLength);
            for (int i = 0; i < participantLength; i++)
            {
                participantIds.Set(i, manager.CurrentOpponents[i].ParticipantId);
                var result = manager.CurrentOpponents[i].ParticipantId.Equals(manager.CurrentMatch.SelfParticipantId) ? MatchOutcome.ParticipantResult.Won : MatchOutcome.ParticipantResult.Lost;
                results.Set(i, result);
            }

            manager.CurrentMatchData.WinnerName = manager.CurrentMatch.Self.DisplayName;
            var bytes = manager.CurrentMatchData.ToByteArray();
            matchData.Value = Convert.ToBase64String(bytes);
            Fsm.Event(eventTarget, isSuccessEvent);
        }
    }
}

#endif