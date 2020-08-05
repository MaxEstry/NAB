#if PLAYMAKER
using UnityEngine;
using System;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Gets the default rewarded ad network as setup in Easy Mobile settings.")]
    public class Advertising_GetDefaultRewardedAdNetwork : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The default rewarded ad network.")]
        [UIHint(UIHint.Variable)]
        [ObjectType(typeof(RewardedAdNetwork))]
        public FsmEnum rewardedAdNetwork;

        [Tooltip("The name string of the rewarded ad network.")]
        [UIHint(UIHint.Variable)]
        public FsmString rewardedAdNetworkString;

        public override void Reset()
        {
            rewardedAdNetwork = null;
            rewardedAdNetworkString = null;
        }

        public override void OnEnter()
        {
            AdSettings.DefaultAdNetworks defaultNetworks = new AdSettings.DefaultAdNetworks(BannerAdNetwork.None, InterstitialAdNetwork.None, RewardedAdNetwork.None);

            #if UNITY_ANDROID
            defaultNetworks = EM_Settings.Advertising.AndroidDefaultAdNetworks;
            #elif UNITY_IOS
            defaultNetworks = EM_Settings.Advertising.IosDefaultAdNetworks;
            #endif

            rewardedAdNetwork.Value = defaultNetworks.rewardedAdNetwork;
            rewardedAdNetworkString.Value = rewardedAdNetwork.ToString();

            Finish();
        }
    }
}
#endif

