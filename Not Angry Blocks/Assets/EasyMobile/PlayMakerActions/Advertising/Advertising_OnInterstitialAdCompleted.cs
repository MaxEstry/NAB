#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Waits until the next interstitial ad completes and send the specified event.")]
    public class Advertising_OnInterstitialAdCompleted : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The ad network of the completed interstitial ad.")]
        [ObjectType(typeof(InterstitialAdNetwork)), UIHint(UIHint.Variable)]
        public FsmEnum adNetwork;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the interstitial ad has completed.")]
        public FsmEvent adCompletedEvent;

        public override void Reset()
        {
            adNetwork = null;
            eventTarget = null;
            adCompletedEvent = null;
        }

        public override void OnEnter()
        {
            Advertising.InterstitialAdCompleted += Advertising_InterstitialAdCompleted;
        }

        public override void OnExit()
        {
            Advertising.InterstitialAdCompleted -= Advertising_InterstitialAdCompleted;
        }

        void Advertising_InterstitialAdCompleted(InterstitialAdNetwork network, AdPlacement loc)
        {
            adNetwork.Value = network;
            Fsm.Event(eventTarget, adCompletedEvent);
        }
    }
}
#endif

