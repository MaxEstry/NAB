#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Determines if the rewarded ad is loaded and ready to be shown.")]
    public class Advertising_IsRewardedAdReady : Advertising_RewardedAdActionBase
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if rewarded ad is loaded and ready to be shown, False otherwise.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isReady;

        [Tooltip("Event sent if rewarded ad is loaded and ready to be shown.")]
        public FsmEvent isReadyEvent;

        [Tooltip("Event sent if rewarded ad is not loaded and ready to be shown.")]
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
            if (param == RewardedAdActionParam.AdNetwork_AdPlacement)
                isReady.Value = Advertising.IsRewardedAdReady(mAdNetwork, mPlacement);
            else
                isReady.Value = Advertising.IsRewardedAdReady();

            if (isReady.Value)
                Fsm.Event(isReadyEvent);
            else
                Fsm.Event(isNotReadyEvent);
        }
    }
}
#endif

