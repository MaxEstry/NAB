#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Native APIs")]
    [Tooltip("Shows a native two-button alert.")]
    public class MobileNativeUI_TwoButtonAlert : FsmStateAction
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

        [ActionSection("Result")]

        [Tooltip("Where to send the event(s).")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the 1st button is clicked.")]
        public FsmEvent firstButtonClickedEvent;

        [Tooltip("Event sent when the 2nd button is clicked.")]
        public FsmEvent secondButtonClickedEvent;

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
            buttonIndex = -1;
            eventTarget = null;
            firstButtonClickedEvent = null;
            secondButtonClickedEvent = null;
            onCompleteEvent = null;
        }

        public override void OnEnter()
        {
            NativeUI.AlertPopup alert = NativeUI.ShowTwoButtonAlert(
                                          title.Value, 
                                          message.Value, 
                                          firstButtonLabel.Value, 
                                          secondButtonLabel.Value
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

            Fsm.Event(eventTarget, onCompleteEvent);
            Finish();
        }
    }
}

#endif
