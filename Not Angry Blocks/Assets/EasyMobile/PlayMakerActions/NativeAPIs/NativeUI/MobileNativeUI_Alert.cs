#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Native APIs")]
    [Tooltip("Shows a native one-button alert.")]
    public class MobileNativeUI_Alert : FsmStateAction
    {
        [Tooltip("The alert title.")]
        [RequiredField]
        public FsmString title;

        [Tooltip("The alert message.")]
        [RequiredField]
        public FsmString message;

        [Tooltip("The button label.")]
        public FsmString buttonLabel;

        [ActionSection("Result")]

        [Tooltip("Where to send the event(s).")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the alert is closed.")]
        public FsmEvent onCompleteEvent;

        [Tooltip("The (zero-based) index of the clicked button.")]
        [UIHint(UIHint.Variable)]
        public FsmInt buttonIndex;

        public override void Reset()
        {
            title = null;
            message = null;
            buttonLabel = "OK";
            buttonIndex = -1;
            eventTarget = null;
            onCompleteEvent = null;
        }

        public override void OnEnter()
        {
            NativeUI.AlertPopup alert = NativeUI.AlertPopup.Alert(title.Value, message.Value);

            if (alert != null)
                alert.OnComplete += OnAlertComplete;
            else
                Finish();
        }

        void OnAlertComplete(int index)
        {
            buttonIndex.Value = index;
            Fsm.Event(eventTarget, onCompleteEvent);
            Finish();
        }
    }
}

#endif
