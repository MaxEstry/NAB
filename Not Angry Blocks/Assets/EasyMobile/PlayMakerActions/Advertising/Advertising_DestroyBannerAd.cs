#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Destroys banner ad.")]
    public class Advertising_DestroyBannerAd : FsmStateAction
    {
        [Tooltip("Set to True to destroy the banner ad of the default ad network, False to specify the network explicitly.")]
        public FsmBool destroyDefaultBannerAd;

        public BannerAdNetwork adNetwork;

        public override void Reset()
        {
            destroyDefaultBannerAd = true;
            adNetwork = BannerAdNetwork.None;
        }

        public override void OnEnter()
        {
            if (destroyDefaultBannerAd.Value)
                Advertising.DestroyBannerAd();
            else
                Advertising.DestroyBannerAd(adNetwork, AdPlacement.Default);

            Finish();
        }
    }
}
#endif

