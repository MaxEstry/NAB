#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using System;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Registers a match delegate to be called when a match arrives.")]
    public class GameServices_TurnBased_RegisterMatchDelegate : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when a match arrives.")]
        public FsmEvent onMatchArrivesEvent;

        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [Tooltip("If this is true, then the game should immediately proceed to the" +
            " game screen to play this match, without prompting the user.")]
        public FsmBool shouldAutoLaunch;

        [Tooltip("This is true only if the local player has removed the match in the" +
            " matches UI while being the turn holder. When this happens you should call " +
            "the LeaveMatchInTurn method to end the local player's turn and pass the match " +
            "to the next appropriate participant according to your game logic. Note that this " +
            "currently happens on Game Center platform only.")]
        public FsmBool playerWantsToQuit;

        public override void Reset()
        {
            base.Reset();
            shouldAutoLaunch = false;
            playerWantsToQuit = false;
            matchObject = null;
        }

        public override void OnEnter()
        {
            GameServices.TurnBased.RegisterMatchDelegate(OnMatchArrives);
        }

        void OnMatchArrives(TurnBasedMatch match, bool shouldAutoLaunchIn, bool playerWantsToQuitIn)
        {
            TurnBasedMatchObject temp = new TurnBasedMatchObject();
            temp.Match = match;
            matchObject.Value = temp;

            shouldAutoLaunch.Value = shouldAutoLaunchIn;
            playerWantsToQuit.Value = playerWantsToQuitIn;         
            Fsm.Event(eventTarget, onMatchArrivesEvent);         
        }
    }
}

#endif