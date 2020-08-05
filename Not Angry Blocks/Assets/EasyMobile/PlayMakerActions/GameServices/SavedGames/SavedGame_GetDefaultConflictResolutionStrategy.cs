#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.SocialPlatforms;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Saved Games")]
    [Tooltip("Get the default Saved Games conflict resolution strategy in Easy Mobile settings.")]
    public class SavedGame_GetDefaultConflictResolutionStrategy : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("True if Saved Games feature is enabled, otherwise False.")]
        [UIHint(UIHint.Variable), ObjectType(typeof(EasyMobile.SavedGameConflictResolutionStrategy))]
        public FsmEnum strategy;

        public override void Reset()
        {
            strategy = null;
        }

        public override void OnEnter()
        {
            strategy.Value = EM_Settings.GameServices.AutoConflictResolutionStrategy;
        }
    }
}
#endif

