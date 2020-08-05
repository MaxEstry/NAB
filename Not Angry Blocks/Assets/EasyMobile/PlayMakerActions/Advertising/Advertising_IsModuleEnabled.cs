#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Determines whether the Advertising module of Easy Mobile is enabled.")]
    public class Advertising_IsModuleEnabled : FsmStateAction
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
            isEnabled = false;
            isNotEnabledEvent = null;
            isEnabledEvent = null;
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
            isEnabled = EM_Settings.IsAdModuleEnable;

            if (isEnabled.Value)
                Fsm.Event(isEnabledEvent);
            else
                Fsm.Event(isNotEnabledEvent);
        }
    }
}
#endif

