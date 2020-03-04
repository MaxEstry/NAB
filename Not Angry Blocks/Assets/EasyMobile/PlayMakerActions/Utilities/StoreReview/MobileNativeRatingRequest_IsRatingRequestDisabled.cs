#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Utilities")]
    [Tooltip("Determines if the rating request dialog has been disabled. " +
        "Disabling occurs if the user either selects the \"refuse\" button or the \"rate\" button. " +
        "On iOS, this is only applicable to versions older than 10.3.")]
    public class MobileNativeRatingRequest_IsRatingRequestDisabled : FsmStateAction
    {
        [Tooltip("Repeat every frame")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if the rating request dialog is disabled, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isRatingRequestDisabled;

        [Tooltip("Event sent if the rating request dialog was disabled.")]
        public FsmEvent ratingRequestDisabledEvent;

        [Tooltip("Event sent if the rating request dialog was not disabled.")]
        public FsmEvent ratingRequestNotDisabledEvent;

        public override void Reset()
        {
            everyFrame = false;
            isRatingRequestDisabled = null;
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
            isRatingRequestDisabled.Value = StoreReview.IsRatingRequestDisabled();

            if (isRatingRequestDisabled.Value)
                Fsm.Event(ratingRequestDisabledEvent);
            else
                Fsm.Event(ratingRequestNotDisabledEvent);
        }
    }
}

#endif
