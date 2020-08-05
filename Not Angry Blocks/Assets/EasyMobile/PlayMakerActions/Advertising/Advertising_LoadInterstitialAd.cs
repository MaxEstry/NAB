#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Loads an interstitial ad.")]
    public class Advertising_LoadInterstitialAd : Advertising_InterstitialAdActionBase
    {
        public override void Reset()
        {
            base.Reset();
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (param == InterstitialAdActionParam.AdNetwork_AdPlacement)
                Advertising.LoadInterstitialAd(mAdNetwork, mPlacement);
            else
                Advertising.LoadInterstitialAd();

            Finish();
        }
    }
}
#endif

