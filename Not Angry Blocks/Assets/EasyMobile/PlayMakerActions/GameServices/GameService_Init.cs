#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("[Only use this if you disabled the Automatic Login option of the Game Service module]" +
        "Initializes the service. This is required before any other actions can be done e.g reporting scores." +
        "During the initialization process, a login popup will show up if the user hasn't logged in, otherwise" +
        "the process will carry on silently." +
        "Note that on iOS, the login popup will show up automatically when the app gets focus for the first 3 times" +
        "while subsequent authentication calls will be ignored.")]
    public class GameService_Init : FsmStateAction
    {
        public override void OnEnter()
        {
            GameServices.Init();
            Finish();
        }
    }
}
#endif

