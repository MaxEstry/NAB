#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("[Only use this if you disabled the Automatic Login option of the Game Service module]" +
        "Internally calls the Init() method. If the user hasn't logged in" +
        "to the service, a login UI will popup. Otherwise, it will initialize silently." +
        "On iOS, the OS automatically shows the login popup when the app gets focus for the first 3 times." +
        "Subsequent init calls will be ignored." +
        "On Android, if the user dismisses the login popup for a number of times determined" +
        "by AndroidMaxLoginRequests, we'll stop showing it (all subsequent init calls will be ignored).")]
    public class GameService_ManagedInit: FsmStateAction
    {
        public override void OnEnter()
        {
            GameServices.ManagedInit();
            Finish();
        }
    }
}
#endif

