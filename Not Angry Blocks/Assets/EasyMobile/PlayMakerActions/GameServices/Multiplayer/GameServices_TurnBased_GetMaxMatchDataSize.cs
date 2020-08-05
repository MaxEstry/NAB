#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;


namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Gets the size of the max match data, in bytes.")]
    public class GameServices_TurnBased_GetMaxMatchDataSize : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The max match data size in bytes.")]
        public FsmInt maxSize;

        public override void Reset()
        {
            base.Reset();
            maxSize = 0;
        }

        public override void OnEnter()
        {
            maxSize.Value = GameServices.TurnBased.GetMaxMatchDataSize();
            Finish();
        }
    }
}

#endif