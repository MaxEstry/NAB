#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Utilities")]
    [Tooltip("Shows the rating request dialog. ")]
    public class MobileNativeRatingRequest_RequestRating : FsmStateAction
    {
        public enum RequestRatingBehaviorOption
        {
            DefaultBehavior,
            CustomBehavior
        }

        [Tooltip("Choose DefaultBehavior to use the default behavior of the rating dialog," +
            "otherwise choose CustomBehavior to implement a custom behavior upon the onCompleteEvent.")]
        public RequestRatingBehaviorOption popupBehavior;

        [ActionSection("Callback - For Custom Behavior")]

        [Tooltip("Event sent when the rating dialog is closed.")]
        public FsmEvent onCompleteEvent;

        [Tooltip("The option the user selected.")]
        [ObjectType(typeof(StoreReview.UserAction))]
        [UIHint(UIHint.Variable)]
        public FsmEnum userAction;

        public override void Reset()
        {
            popupBehavior = RequestRatingBehaviorOption.DefaultBehavior;
            userAction = null;
        }

        public override void OnEnter()
        {
            if (popupBehavior == RequestRatingBehaviorOption.DefaultBehavior)
            {
                StoreReview.RequestRating();
                Finish();
            }
            else
            {
                StoreReview.RequestRating(null, RequestRatingCallback);
            }
        }

        void RequestRatingCallback(StoreReview.UserAction userActionCallback)
        {
            userAction.Value = userActionCallback;
            Fsm.Event(onCompleteEvent);
            Finish();
        }
    }
}

#endif
