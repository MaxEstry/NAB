#if PLAYMAKER
using UnityEngine;
using System;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Gets the default interstitial ad network as setup in Easy Mobile settings.")]
    public class Advertising_GetDefaultInterstitialAdNetwork : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The default interstitial ad network.")]
        [UIHint(UIHint.Variable)]
        [ObjectType(typeof(InterstitialAdNetwork))]
        public FsmEnum interstitialAdNetwork;

        [Tooltip("The name string of the interstitial ad network.")]
        [UIHint(UIHint.Variable)]
        public FsmString interstitialAdNetworkString;

        public override void Reset()
        {
            interstitialAdNetwork = null;
            interstitialAdNetworkString = null;
        }

        public override void OnEnter()
        {
            AdSettings.DefaultAdNetworks defaultNetworks = new AdSettings.DefaultAdNetworks(BannerAdNetwork.None, InterstitialAdNetwork.None, RewardedAdNetwork.None);

            #if UNITY_ANDROID
            defaultNetworks = EM_Settings.Advertising.AndroidDefaultAdNetworks;
            #elif UNITY_IOS
            defaultNetworks = EM_Settings.Advertising.IosDefaultAdNetworks;
            #endif

            interstitialAdNetwork.Value = defaultNetworks.interstitialAdNetwork;
            interstitialAdNetworkString.Value = interstitialAdNetwork.ToString();

            Finish();
        }
    }
}
#endif

