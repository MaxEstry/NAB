#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Creates the match with standard UI.")]
    public class GameServices_TurnBased_CreateWithMatchmakerUI : FsmStateAction
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

        [Tooltip("Event sent if match canceled.")]
        public FsmEvent isCanceledEvent;

        [Tooltip("Event sent if match created not successful. ")]
        public FsmEvent isErrorEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            minPlayers = 2;
            maxPlayers = 2;
            variant = 0;
            exclusiveBitmask = 0;


        }

        public override void OnEnter()
        {
            MatchRequest request = new MatchRequest();
            request.MinPlayers = (uint)minPlayers.Value;
            request.MaxPlayers = (uint)maxPlayers.Value;
            request.Variant = (uint)variant.Value;
            request.ExclusiveBitmask = (uint)exclusiveBitmask.Value;
            GameServices.TurnBased.CreateWithMatchmakerUI(request, OnCancel, OnError);
        }

        void OnCancel()
        {
            Fsm.Event(eventTarget, isCanceledEvent);
        }

        void OnError(string error)
        {
            Fsm.Event(eventTarget, isErrorEvent);
        }
    }
}

#endif