#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Removes ads permanently. Use this for the RemoveAds button." +
        "This will hide the default banner ad if it is being shown and " +
        "prohibit future loading and showing of all ads except rewarded ads.")]
    public class Advertising_RemoveAds : FsmStateAction
    {
        public override void OnEnter()
        {
            Advertising.RemoveAds();
            Finish();
        }
    }
}
#endif

