#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Shows a rewarded ad. Use the 'On Rewarded Ad Completed' action to acknowledge when the ad finishes and reward the user.")]
    public class Advertising_ShowRewardedAd : Advertising_RewardedAdActionBase
    {
        public override void Reset()
        {
            base.Reset();
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (param == RewardedAdActionParam.AdNetwork_AdPlacement)
                Advertising.ShowRewardedAd(mAdNetwork, mPlacement);
            else
                Advertising.ShowRewardedAd();

            Finish();
        }
    }
}
#endif

