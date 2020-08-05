#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Determines whether the auto initialization feature is enabled.")]
    public class GameService_IsAutoInit : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if the auto initialization is enabled, False otherwise.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isAutoInit;

        [Tooltip("Event sent if auto initialization is enabled.")]
        public FsmEvent isAutoInitEvent;

        [Tooltip("Event sent if auto initialization is not enabled.")]
        public FsmEvent isNotAutoInitEvent;

        public override void Reset()
        {
            everyFrame = false;
            isAutoInit = false;
            isNotAutoInitEvent = null;
            isAutoInitEvent = null;
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
            isAutoInit = EM_Settings.GameServices.IsAutoInit;

            if (isAutoInit.Value)
                Fsm.Event(isAutoInitEvent);
            else
                Fsm.Event(isNotAutoInitEvent);
        }
    }
}
#endif

