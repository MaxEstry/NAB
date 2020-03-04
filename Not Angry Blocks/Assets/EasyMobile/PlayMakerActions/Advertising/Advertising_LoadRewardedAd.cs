#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Loads a rewarded ad.")]
    public class Advertising_LoadRewardedAd : Advertising_RewardedAdActionBase
    {
        public override void Reset()
        {
            base.Reset();
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (param == RewardedAdActionParam.AdNetwork_AdPlacement)
                Advertising.LoadRewardedAd(mAdNetwork, mPlacement);
            else
                Advertising.LoadRewardedAd();

            Finish();
        }
    }
}
#endif

