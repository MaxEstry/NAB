#if PLAYMAKER
using UnityEngine;
using System;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Gets the default banner ad network as setup in Easy Mobile settings.")]
    public class Advertising_GetDefaultBannerAdNetwork : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The default banner ad network.")]
        [UIHint(UIHint.Variable)]
        [ObjectType(typeof(BannerAdNetwork))]
        public FsmEnum bannerAdNetwork;

        [Tooltip("The name string of the banner ad network.")]
        [UIHint(UIHint.Variable)]
        public FsmString bannerAdNetworkString;

        public override void Reset()
        {
            bannerAdNetwork = null;
            bannerAdNetworkString = null;
        }

        public override void OnEnter()
        {
            AdSettings.DefaultAdNetworks defaultNetworks = new AdSettings.DefaultAdNetworks(BannerAdNetwork.None, InterstitialAdNetwork.None, RewardedAdNetwork.None);

            #if UNITY_ANDROID
            defaultNetworks = EM_Settings.Advertising.AndroidDefaultAdNetworks;
            #elif UNITY_IOS
            defaultNetworks = EM_Settings.Advertising.IosDefaultAdNetworks;
            #endif

            bannerAdNetwork.Value = defaultNetworks.bannerAdNetwork;
            bannerAdNetworkString.Value = bannerAdNetwork.ToString();

            Finish();
        }
    }
}
#endif

