#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Gets all matches.")]
    public class GameServices_TurnBased_GetAllMatches : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The number of matches found.")]
        [UIHint(UIHint.Variable)]
        public FsmInt matchCount;

        [Tooltip("The Unity Object containing the all matches")]
        [ArrayEditor(VariableType.Object)]
        public FsmArray matches;

        [Tooltip("True if success.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Event sent if get matches successful. ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if get matches not successful. ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            matchCount = 0;
            matches = null;
        }

        public override void OnEnter()
        {
            GameServices.TurnBased.GetAllMatches((allMatches) =>
            {
                if (allMatches != null)
                {
                    matchCount.Value = allMatches.Length;

                    matches.Resize(matchCount.Value);

                    for (int i = 0; i < matchCount.Value; i++)
                    {
                        TurnBasedMatchObject matchObject = new TurnBasedMatchObject();

                        matchObject.Match = allMatches[i];

                        matches.Set(i, matchObject);
                    }
                    Fsm.Event(eventTarget, isSuccessEvent);
                    Finish();
                }
                else
                {
                    Fsm.Event(eventTarget, isNotSuccessEvent);
                    Finish();
                }
            });

        }
    }
}

#endif