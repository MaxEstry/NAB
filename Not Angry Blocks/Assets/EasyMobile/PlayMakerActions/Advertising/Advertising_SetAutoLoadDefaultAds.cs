#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Enables/Disables auto loading of default ads.")]
    public class Advertising_SetAutoLoadDefaultAds : FsmStateAction
    {
        [Tooltip("Set to True to enable auto loading default ads, False to disable.")]
        public FsmBool isAutoLoadDefaultAds;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        public override void Reset()
        {
            isAutoLoadDefaultAds = false;
            everyFrame = false;
        }

#pragma warning disable 0618

        public override void OnEnter()
        {
            Advertising.EnableAutoLoadDefaultAds(isAutoLoadDefaultAds.Value);

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            Advertising.EnableAutoLoadDefaultAds(isAutoLoadDefaultAds.Value);
        }

#pragma warning restore 0618
    }
}
#endif
