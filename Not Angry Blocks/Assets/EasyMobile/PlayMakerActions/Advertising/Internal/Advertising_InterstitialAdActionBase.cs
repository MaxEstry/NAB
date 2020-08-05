#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    public abstract class Advertising_InterstitialAdActionBase : FsmStateAction
    {
        public enum InterstitialAdActionParam
        {
            Default,
            AdNetwork_AdPlacement
        }

        [Tooltip("Action parameters, choose Default to use the default settings")]
        public InterstitialAdActionParam param;

        [Tooltip("Interstitial ad network")]
        [ObjectType(typeof(InterstitialAdNetwork))]
        public FsmEnum adNetwork;

        [Tooltip("The name of the ad placement. Leave empty to use the Default placement.")]
        public FsmString adPlacement;

        /// <summary>
        /// The actual ad placement converted from name.
        /// </summary>
        protected AdPlacement mPlacement;

        /// <summary>
        /// The actual interstitial ad network value.
        /// </summary>
        protected InterstitialAdNetwork mAdNetwork;

        public override void Reset()
        {
            param = InterstitialAdActionParam.Default;
            adNetwork = InterstitialAdNetwork.None;
            adPlacement = null;
        }

        public override void OnEnter()
        {
            mPlacement = AdPlacement.PlacementWithName(adPlacement.Value);
            mAdNetwork = (InterstitialAdNetwork)adNetwork.Value;
        }
    }
}
#endif

