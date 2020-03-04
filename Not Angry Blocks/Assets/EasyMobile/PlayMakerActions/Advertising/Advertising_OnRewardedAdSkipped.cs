#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Waits until the next rewarded ad is skipped.")]
    public class Advertising_OnRewardedAdSkipped : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The ad network of the skipped rewarded ad.")]
        [ObjectType(typeof(RewardedAdNetwork)), UIHint(UIHint.Variable)]
        public FsmEnum adNetwork;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the rewarded ad has been skipped.")]
        public FsmEvent adSkippedEvent;

        public override void Reset()
        {
            adNetwork = null;   
            eventTarget = null;
            adSkippedEvent = null;
        }

        public override void OnEnter()
        {
            Advertising.RewardedAdSkipped += Advertising_RewardedAdSkipped;
        }

        public override void OnExit()
        {
            Advertising.RewardedAdSkipped -= Advertising_RewardedAdSkipped;
        }

        void Advertising_RewardedAdSkipped(RewardedAdNetwork network, AdPlacement p)
        {
            adNetwork.Value = network;
            Fsm.Event(eventTarget, adSkippedEvent);
        }
    }
}
#endif

