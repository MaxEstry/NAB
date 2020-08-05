#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Determines if the In App Purchasing module of Easy Mobile is enabled.")]
    public class InAppPurchasing_IsModuleEnabled : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if the module is enabled, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isEnabled;

        [Tooltip("Event sent if the module is enabled.")]
        public FsmEvent isEnabledEvent;

        [Tooltip("Event sent if the module is not enabled.")]
        public FsmEvent isNotEnabledEvent;

        public override void Reset()
        {
            everyFrame = false;
            isEnabled = null;
            isEnabledEvent = null;
            isNotEnabledEvent = null;
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
            isEnabled.Value = EM_Settings.IsIAPModuleEnable;

            if (isEnabled.Value)
                Fsm.Event(isEnabledEvent);
            else
                Fsm.Event(isNotEnabledEvent);
        }
    }
}
#endif

