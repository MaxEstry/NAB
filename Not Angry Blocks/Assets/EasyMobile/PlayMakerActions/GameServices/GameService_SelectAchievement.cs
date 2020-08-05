#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Returns the name & ID of the selected achievement. " +
        "It is necessary to generate the Game Service constants in Easy Mobile settings before using this action.")]
    public class GameService_SelectAchievement : FsmStateAction
    {
        [Tooltip("Select an achievement.")]
        public FsmString achievement;

        [ActionSection("Result")]

        [Tooltip("The name of the selected achievement.")]
        [UIHint(UIHint.Variable)]
        public FsmString name;

        [Tooltip("The ID of the selected achievement.")]
        [UIHint(UIHint.Variable)]
        public FsmString id;

        public override void Reset()
        {
            achievement = null;
            name = null;
            id = null;
        }

        public override void OnEnter()
        {
            name.Value = achievement.Value;
            id.Value = GameServices.GetAchievementByName(name.Value).Id;

            Finish();
        }
    }
}
#endif
