#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Waits until the next rewarded ad completes, which is when you should reward the user.")]
    public class Advertising_OnRewardedAdCompleted : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The ad network of the completed rewarded ad.")]
        [ObjectType(typeof(RewardedAdNetwork)), UIHint(UIHint.Variable)]
        public FsmEnum adNetwork;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the rewarded ad has completed.")]
        public FsmEvent adCompletedEvent;

        public override void Reset()
        {
            adNetwork = null;   
            eventTarget = null;
            adCompletedEvent = null;
        }

        public override void OnEnter()
        {
            Advertising.RewardedAdCompleted += Advertising_RewardedAdCompleted;
        }

        public override void OnExit()
        {
            Advertising.RewardedAdCompleted -= Advertising_RewardedAdCompleted;
        }

        void Advertising_RewardedAdCompleted(RewardedAdNetwork network, AdPlacement loc)
        {
            adNetwork.Value = network;
            Fsm.Event(eventTarget, adCompletedEvent);
        }
    }
}
#endif

