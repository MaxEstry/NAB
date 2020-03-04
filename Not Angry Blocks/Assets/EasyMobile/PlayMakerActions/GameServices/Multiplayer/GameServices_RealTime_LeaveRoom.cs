#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;


namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Call this method to leave the room after you have started room setup.")]
    public class GameServices_RealTime_LeaveRoom : FsmStateAction
    {
        public override void OnEnter()
        {
            GameServices.RealTime.LeaveRoom();
            Finish();
        }
    }
}

#endif