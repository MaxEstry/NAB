#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayMaker.Actions
{
    [ActionCategory("Easy Mobile - Notifications")]
    [Tooltip("Determines if the service is initialized and notifications can be posted.")]
    public class Notification_IsInitialized : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [Tooltip("True if notification is initialized.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isInitialized;

        [Tooltip("Event sent if the notification is initialized")]
        public FsmEvent isInitializedEvent;

        [Tooltip("Event sent if the notification is not initialized")]
        public FsmEvent isNotInitializedEvent;

        public override void Reset()
        {
            everyFrame = false;
            isInitialized = false;
            isInitializedEvent = null;
            isNotInitializedEvent = null;
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
            isInitialized = Notifications.IsInitialized();
            if (isInitialized.Value)
                Fsm.Event(isInitializedEvent);
            else
                Fsm.Event(isNotInitializedEvent);
        }
    }
}
#endif
