#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Returns the name & ID of the selected leaderboard. " +
        "It is necessary to generate the Game Service constants in Easy Mobile settings before using this action.")]
    public class GameService_SelectLeaderboard : FsmStateAction
    {
        [Tooltip("Select a leaderboard.")]
        public FsmString leaderboard;

        [ActionSection("Result")]

        [Tooltip("The name of the selected leaderboard.")]
        [UIHint(UIHint.Variable)]
        public FsmString name;

        [Tooltip("The ID of the selected leaderboard.")]
        [UIHint(UIHint.Variable)]
        public FsmString id;

        public override void Reset()
        {
            leaderboard = null;
            name = null;
            id = null;
        }

        public override void OnEnter()
        {
            name.Value = leaderboard.Value;
            id.Value = GameServices.GetLeaderboardByName(name.Value).Id;

            Finish();
        }
    }
}
#endif
