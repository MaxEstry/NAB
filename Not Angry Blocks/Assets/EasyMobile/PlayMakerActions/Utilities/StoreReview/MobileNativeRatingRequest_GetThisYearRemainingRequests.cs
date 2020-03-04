#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Utilities")]
    [Tooltip("Gets the number of unused requests in this year. " +
        "Note that this is not applicable to iOS 10.3 or newer.")]
    public class MobileNativeRatingRequest_GetThisYearRemainingRequests : FsmStateAction
    {
        [Tooltip("Repeat every frame")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("The remaining requests in this year.")]
        [UIHint(UIHint.Variable)]
        public FsmInt remainingRequests;

        public override void Reset()
        {
            everyFrame = false;
            remainingRequests = null;
        }

        public override void OnEnter()
        {
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
            remainingRequests.Value = StoreReview.GetThisYearRemainingRequests();
        }
    }
}
#endif

