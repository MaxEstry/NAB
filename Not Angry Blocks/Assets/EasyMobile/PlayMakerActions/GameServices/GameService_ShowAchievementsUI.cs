#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Shows the achivements UI.")]
    public class GameService_ShowAchievementsUI : FsmStateAction
    {
        public override void OnEnter()
        {
            GameServices.ShowAchievementsUI();
            Finish();
        }
    }
}
#endif