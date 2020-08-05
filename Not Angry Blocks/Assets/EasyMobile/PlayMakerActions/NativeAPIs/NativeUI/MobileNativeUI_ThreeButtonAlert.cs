#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Native APIs")]
    [Tooltip("Shows a native three-button alert.")]
    public class MobileNativeUI_ThreeButtonAlert : FsmStateAction
    {
        [Tooltip("The alert title.")]
        [RequiredField]
        public FsmString title;

        [Tooltip("The alert message.")]
        [RequiredField]
        public FsmString message;

        [Tooltip("The label of the 1st button.")]
        [RequiredField]
        public FsmString firstButtonLabel;

        [Tooltip("The label of the 2nd button.")]
        [RequiredField]
        public FsmString secondButtonLabel;

        [Tooltip("The label of the 3rd button.")]
        [RequiredField]
        public FsmString thirdButtonLabel;

        [ActionSection("Result")]

        [Tooltip("Where to send the event(s).")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the 1st button is clicked.")]
        public FsmEvent firstButtonClickedEvent;

        [Tooltip("Event sent when the 2nd button is clicked.")]
        public FsmEvent secondButtonClickedEvent;

        [Tooltip("Event sent when the 3rd button is clicked.")]
        public FsmEvent thirdButtonClickedEvent;

        [Tooltip("Event sent when the alert is closed.")]
        public FsmEvent onCompleteEvent;

        [Tooltip("The (zero-based) index of the clicked button.")]
        [UIHint(UIHint.Variable)]
        public FsmInt buttonIndex;

        public override void Reset()
        {
            title = null;
            message = null;
            firstButtonLabel = null;
            secondButtonLabel = null;
            thirdButtonLabel = null;
            buttonIndex = -1;
            eventTarget = null;
            firstButtonClickedEvent = null;
            secondButtonClickedEvent = null;
            thirdButtonClickedEvent = null;
        }

        public override void OnEnter()
        {
            NativeUI.AlertPopup alert = NativeUI.ShowThreeButtonAlert(
                                          title.Value, 
                                          message.Value,
                                          firstButtonLabel.Value,
                                          secondButtonLabel.Value,
                                          thirdButtonLabel.Value
                                      );

            if (alert != null)
                alert.OnComplete += OnAlertComplete;
            else
                Finish();
        }

        void OnAlertComplete(int index)
        {
            buttonIndex.Value = index;

            if (index == 0)
                Fsm.Event(eventTarget, firstButtonClickedEvent);
            else if (index == 1)
                Fsm.Event(eventTarget, secondButtonClickedEvent);
            else if (index == 2)
                Fsm.Event(eventTarget, thirdButtonClickedEvent);

            Fsm.Event(eventTarget, onCompleteEvent);
            Finish();
        }
    }
}

#endif
