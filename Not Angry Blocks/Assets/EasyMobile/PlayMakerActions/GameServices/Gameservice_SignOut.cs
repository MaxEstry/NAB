#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Signs the user out. Available on Android only.")]
    public class GameService_SignOut : FsmStateAction
    {
        public override void OnEnter()
        {
            GameServices.SignOut();
            Finish();
        }
    }
}
#endif