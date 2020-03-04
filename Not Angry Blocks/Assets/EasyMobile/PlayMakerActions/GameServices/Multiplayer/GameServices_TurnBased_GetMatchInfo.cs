#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Gets the information of a Turn-Based match")]
    public class GameServices_TurnBased_GetMatchInfo : FsmStateAction
    {
        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [ActionSection("Result")]

        [Tooltip("Match ID")]
        public FsmString matchId;

        [Tooltip("The total number of players required for this match.")]
        public FsmInt playerCount;

        [Tooltip("Returns true if this match has at least one slot to be filled by a participant.")]
        public FsmBool hasVacantSlot;

        [Tooltip("Returns true if the current turn belongs to the local player.")]
        public FsmBool isMyTurn;

        [Tooltip("Self Participant Id.")]
        public FsmString selfParticipantId;

        [Tooltip("List of participant Ids")]
        [ArrayEditor(VariableType.String)]
        public FsmArray participantIds;

        [Tooltip("The current Participant Id.")]
        public FsmString currentParticipantId;

        [Tooltip("Match status")]
        [ObjectType(typeof(TurnBasedMatch.MatchStatus))]
        public FsmEnum status;

        public override void Reset()
        {
            base.Reset();
            matchObject = null;
            matchId = null;
            playerCount = 0;
            hasVacantSlot = false;
            isMyTurn = false;
            currentParticipantId = null;
            selfParticipantId = null;
        }

        public override void OnEnter()
        {
            TurnBasedMatchObject temp = (TurnBasedMatchObject)matchObject.Value;

            TurnBasedMatch match = temp.Match;

            matchId.Value = match.MatchId;
            playerCount = match.PlayerCount;
            hasVacantSlot = match.HasVacantSlot;
            isMyTurn = match.IsMyTurn;
            currentParticipantId = match.CurrentParticipantId;
            selfParticipantId = match.SelfParticipantId;

            
            if(match.PlayerCount > 0)
            {
                participantIds.Resize(match.PlayerCount);

                Participant[] participants = match.Participants;
                for (int i = 0;i < match.PlayerCount; i++){
                    participantIds.Set(i, participants[i].ParticipantId);
                }

            }
            Finish();
        }      
    }
}

#endif