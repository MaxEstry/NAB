#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Hides banner ad.")]
    public class Advertising_HideBannerAd : FsmStateAction
    {
        [Tooltip("Set to True to hide the banner ad of the default ad network, False to specify the network explicitly.")]
        [UIHint(UIHint.FsmBool)]
        public FsmBool hideDefaultBannerAd;

        public BannerAdNetwork adNetwork;

        public override void Reset()
        {
            hideDefaultBannerAd = true;
            adNetwork = BannerAdNetwork.None;
        }

        public override void OnEnter()
        {
            if (hideDefaultBannerAd.Value)
                Advertising.HideBannerAd();
            else
                Advertising.HideBannerAd(adNetwork, AdPlacement.Default);

            Finish();
        }
    }
}
#endif

