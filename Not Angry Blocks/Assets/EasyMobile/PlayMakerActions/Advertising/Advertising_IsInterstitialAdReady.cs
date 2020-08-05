#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Determines if the interstitial ad is loaded and ready to be shown.")]
    public class Advertising_IsInterstitialAdReady : Advertising_InterstitialAdActionBase
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if interstitial ad is loaded and ready to be shown, False otherwise.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isReady;

        [Tooltip("Event sent if interstitial ad is loaded and ready to be shown.")]
        public FsmEvent isReadyEvent;

        [Tooltip("Event sent if interstitial ad is not loaded and ready to be shown.")]
        public FsmEvent isNotReadyEvent;

        public override void Reset()
        {
            base.Reset();

            isReady = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            DoMyAction();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoMyAction();   
        }

        void DoMyAction()
        {
            if (param == InterstitialAdActionParam.AdNetwork_AdPlacement)
                isReady.Value = Advertising.IsInterstitialAdReady(mAdNetwork, mPlacement);
            else
                isReady.Value = Advertising.IsInterstitialAdReady();

            if (isReady.Value)
                Fsm.Event(isReadyEvent);
            else
                Fsm.Event(isNotReadyEvent);
        }
    }
}
#endif

