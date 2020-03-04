#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Determines whether UnityIAP is initialized. All further actions like purchasing or restoring " +
        "can only be done if UnityIAP is initialized.")]
    public class InAppPurchasing_IsInitialized : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if initialized, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isInitialized;

        [Tooltip("Event sent if UnityIAP is initialized.")]
        public FsmEvent isInitializedEvent;

        [Tooltip("Event sent if UnityIAP is not initialized.")]
        public FsmEvent isNotInitializedEvent;

        public override void Reset()
        {
            everyFrame = false;
            isInitialized = null;
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
            isInitialized.Value = InAppPurchasing.IsInitialized();

            if (isInitialized.Value)
                Fsm.Event(isInitializedEvent);
            else
                Fsm.Event(isNotInitializedEvent);
        }
    }
}
#endif

