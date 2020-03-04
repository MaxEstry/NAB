#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;


namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Shows the standard UI where player can pick a match or accept an invitations.")]
    public class GameServices_TurnBased_ShowMatchesUI : FsmStateAction
    {
        public override void OnEnter()
        {
            GameServices.TurnBased.ShowMatchesUI();
            Finish();
        }
    }
}

#endif