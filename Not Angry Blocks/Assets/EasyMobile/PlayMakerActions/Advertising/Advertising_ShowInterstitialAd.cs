#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Shows an interstitial ad. Use the 'On Interstitial Ad Completed' action to acknowledge when the ad finishes.")]
    public class Advertising_ShowInterstitialAd : Advertising_InterstitialAdActionBase
    {
        public override void Reset()
        {
            base.Reset();
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (param == InterstitialAdActionParam.AdNetwork_AdPlacement)
                Advertising.ShowInterstitialAd(mAdNetwork, mPlacement);
            else
                Advertising.ShowInterstitialAd();

            Finish();
        }
    }
}
#endif

