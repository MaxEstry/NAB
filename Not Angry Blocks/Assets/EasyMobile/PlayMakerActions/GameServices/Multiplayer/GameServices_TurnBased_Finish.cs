#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using System;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Finishes a match.")]
    public class GameServices_TurnBased_Finish : FsmStateAction
    {
        public enum ResultParam
        {
            ParticipantResult,
            ParticipantPlacement,
            ParticipantResultAndPlacement
        }

        [Tooltip("Result parameter(s)")]
        public ResultParam param;

        [Tooltip("Participant IDs.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray participantIds;

        [Tooltip("Participant results.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(typeof(MatchOutcome.ParticipantResult))]
        public FsmArray results;

        [Tooltip("Participant placements.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Int)]
        public FsmArray placements;

        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [Tooltip("Match data. The data to send in form of a base64 string.")]
        [UIHint(UIHint.Variable)]
        public FsmString data;

        [ActionSection("Result")]

        [Tooltip("True if success.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Event sent if finish match successful. ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if finish match not successful. ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            matchObject = null;
            data = null;
            param = ResultParam.ParticipantResult;
            participantIds = null;
            results = null;
            placements = null;
        }

        public override void OnEnter()
        {
            TurnBasedMatchObject temp = (TurnBasedMatchObject)matchObject.Value;

            TurnBasedMatch match = temp.Match;

            MatchOutcome outcome = null;
            switch (param)
            {
                case ResultParam.ParticipantResult:
                    outcome = CreateMatchOutcomeWithResults();
                    break;
                case ResultParam.ParticipantPlacement:
                    outcome = CreateMatchOutcomeWithPlacements();
                    break;
                default:
                    outcome = CreateMatchOutcomeWithResultandPlacement();
                    break;
            }
            if (outcome != null)
            {
                byte[] binaryData = Convert.FromBase64String(data.Value);
                GameServices.TurnBased.Finish(match, binaryData, outcome, OutComeCallBack);
                return;
            }
        }

        MatchOutcome CreateMatchOutcomeWithResults()
        {
            MatchOutcome buffer = new MatchOutcome();
            for(int i = 0; i < participantIds.Length; i++)
            {
                buffer.SetParticipantResult((string)participantIds.Values[i], (MatchOutcome.ParticipantResult)results.Values[i]);
            }
            return buffer;
        }

        MatchOutcome CreateMatchOutcomeWithPlacements()
        {
            MatchOutcome buffer = new MatchOutcome();
            for (int i = 0; i < participantIds.Length; i++)
            {
                buffer.SetParticipantPlacement((string)participantIds.Values[i], (uint)placements.Values[i]);
            }
            return buffer;
        }

        MatchOutcome CreateMatchOutcomeWithResultandPlacement()
        {
            MatchOutcome buffer = new MatchOutcome();
            for (int i = 0; i < participantIds.Length; i++)
            {
                buffer.SetParticipantResult((string)participantIds.Values[i], (MatchOutcome.ParticipantResult)results.Values[i]);
                buffer.SetParticipantPlacement((string)participantIds.Values[i], (uint)placements.Values[i]);
            }
            return buffer;
        }

        void OutComeCallBack(bool isFinish)
        {
            if (isFinish)
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