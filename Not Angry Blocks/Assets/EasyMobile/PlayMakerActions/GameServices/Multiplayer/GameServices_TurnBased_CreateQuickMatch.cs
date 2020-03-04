#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using System;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Starts a game with randomly selected opponent(s). No UI will be shown.")]
    public class GameServices_TurnBased_CreateQuickMatch : FsmStateAction
    {
        [Tooltip("The min players of the match request")]
        public FsmInt minPlayers;

        [Tooltip("The max players of the match request")]
        public FsmInt maxPlayers;

        [Tooltip("The match variant.The meaning of this parameter is defined by the game.")]
        public FsmInt variant;

        [Tooltip("If your game has multiple player roles (such as farmer, archer, and wizard) " +
        "and you want to restrict auto-matched games to one player of each role, " +
        "add an exclusive bitmask to your match request. When auto-matching with this option, " +
        "players will only be considered for a match when the logical AND of their exclusive " +
        "bitmasks is equal to 0. In other words, this value represents the exclusive role the " +
        "player making this request wants to play in the created match. If this value is 0 (default) " +
        "it will be ignored. If you're creating a match with the standard matchmaker UI, this value " +
        "will also be ignored.")]
        public FsmInt exclusiveBitmask;

        [ActionSection("Result")]

        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [Tooltip("True if success.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Event sent if match created successful.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if match created not successful.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            minPlayers = 2;
            maxPlayers = 2;
            variant = 0;
            exclusiveBitmask = 0;
            matchObject = null;
        }

        public override void OnEnter()
        {
            MatchRequest request = new MatchRequest();
            request.MinPlayers = (uint)minPlayers.Value;
            request.MaxPlayers = (uint)maxPlayers.Value;
            request.Variant = (uint)variant.Value;
            request.ExclusiveBitmask = (uint)exclusiveBitmask.Value;
            GameServices.TurnBased.CreateQuickMatch(request, OnCreateQuickMatch);

        }

        void OnCreateQuickMatch(bool isCreatedSuccess, TurnBasedMatch match)
        {
            if (isCreatedSuccess)
            {
                TurnBasedMatchObject temp = new TurnBasedMatchObject();

                temp.Match = match;

                matchObject.Value = temp;

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