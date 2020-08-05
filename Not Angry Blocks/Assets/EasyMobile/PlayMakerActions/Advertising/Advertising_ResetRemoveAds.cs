#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Resets the remove ads status, allows showing ads again.")]
    public class Advertising_ResetRemoveAds : FsmStateAction
    {
        public override void OnEnter()
        {
            Advertising.ResetRemoveAds();
            Finish();
        }
    }
}
#endif

