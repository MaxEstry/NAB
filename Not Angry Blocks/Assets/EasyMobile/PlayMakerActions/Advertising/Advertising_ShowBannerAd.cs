#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Shows a banner ad.")]
    public class Advertising_ShowBannerAd : FsmStateAction
    {
        public enum ShowBannerAdParam
        {
            Position,
            Position_Size,
            Position_Size_AdNetwork
        }

        [Tooltip("Show banner ad parameter(s)")]
        public ShowBannerAdParam param;

        [Tooltip("Banner ad position")]
        [ObjectType(typeof(BannerAdPosition))]
        public FsmEnum bannerPosition;

        [Tooltip("Set to True to use default banner ad size")]
        public FsmBool useDefaultBannerSize;

        [Tooltip("Banner ad width")]
        public FsmInt bannerWidth;

        [Tooltip("Banner ad height")]
        public FsmInt bannerHeight;

        [Tooltip("Banner ad network")]
        [ObjectType(typeof(BannerAdNetwork))]
        public FsmEnum adNetwork;

        public override void Reset()
        {
            param = ShowBannerAdParam.Position;
            bannerPosition = BannerAdPosition.Bottom;
            useDefaultBannerSize = true;
            bannerWidth = 320;
            bannerHeight = 50;
            adNetwork = BannerAdNetwork.None;
        }

        public override void OnEnter()
        {
            var pos = (BannerAdPosition)bannerPosition.Value;
            switch (param)
            {
                case ShowBannerAdParam.Position:
                    Advertising.ShowBannerAd(pos);
                    break;
                case ShowBannerAdParam.Position_Size:
                    Advertising.ShowBannerAd(pos, useDefaultBannerSize.Value ? BannerAdSize.SmartBanner : new BannerAdSize(bannerWidth.Value, bannerHeight.Value));
                    break;
                case ShowBannerAdParam.Position_Size_AdNetwork:
                    Advertising.ShowBannerAd((BannerAdNetwork)adNetwork.Value, pos, useDefaultBannerSize.Value ? BannerAdSize.SmartBanner : new BannerAdSize(bannerWidth.Value, bannerHeight.Value));
                    break;
            }

            Finish();
        }
    }
}
#endif

