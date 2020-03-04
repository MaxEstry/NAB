#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    public abstract class Advertising_RewardedAdActionBase : FsmStateAction
    {
        public enum RewardedAdActionParam
        {
            Default,
            AdNetwork_AdPlacement
        }

        [Tooltip("Action parameters, choose Default to use the default settings")]
        public RewardedAdActionParam param;

        [Tooltip("Rewarded ad network")]
        [ObjectType(typeof(RewardedAdNetwork))]
        public FsmEnum adNetwork;

        [Tooltip("The name of the ad placement. Leave empty to use the Default placement.")]
        public FsmString adPlacement;

        /// <summary>
        /// The actual ad placment converted from the name.
        /// </summary>
        protected AdPlacement mPlacement;

        /// <summary>
        /// The actual rewarded ad value.
        /// </summary>
        protected RewardedAdNetwork mAdNetwork;

        public override void Reset()
        {
            param = RewardedAdActionParam.Default;
            adNetwork = RewardedAdNetwork.None;
            adPlacement = null;
        }

        public override void OnEnter()
        {
            mAdNetwork = (RewardedAdNetwork)adNetwork.Value;
            mPlacement = AdPlacement.PlacementWithName(adPlacement.Value);
        }
    }
}
#endif

