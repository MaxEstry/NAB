#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Utilities")]
    [Tooltip("Determines if a rating request can be made (a popup can be shown), which means it was not previously disabled by the user, " +
        "the user hasn't accepted to rate before, and the annual cap hasn't been met yet.")]
    public class MobileNativeRatingRequest_CanRequestRating : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if a request rating dialog can be shown, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool canRequestRating;

        [Tooltip("Event sent if a rating request can be made.")]
        public FsmEvent canRequestRatingEvent;

        [Tooltip("Event sent if a rating request can not be made.")]
        public FsmEvent canNotRequestRatingEvent;

        public override void Reset()
        {
            everyFrame = false;
            canRequestRating = null;
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
            canRequestRating.Value = StoreReview.CanRequestRating();

            if (canRequestRating.Value)
                Fsm.Event(canRequestRatingEvent);
            else
                Fsm.Event(canNotRequestRatingEvent);
        }
    }
}
#endif

