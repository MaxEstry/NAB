#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Shows the leaderboard UI.")]
    public class GameService_ShowLeaderboardUI : FsmStateAction
    {
        public override void OnEnter()
        {
            GameServices.ShowLeaderboardUI();
            Finish();
        }
    }
}
#endif

