#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Determines whether the service is initialized (user is authenticated) and ready to use.")]
    public class GameService_IsInitialized : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if the service is initialized.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isInitialized;

        [Tooltip("Event sent if the service is initialized.")]
        public FsmEvent isInitializedEvent;

        [Tooltip("Event sent if the service is not initialized.")]
        public FsmEvent isNotInitializedEvent;

        public override void Reset()
        {
            everyFrame = false;
            isInitialized = false;
            isNotInitializedEvent = null;
            isInitializedEvent = null;
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
            isInitialized = GameServices.IsInitialized();

            if (isInitialized.Value)
                Fsm.Event(isInitializedEvent);
            else
                Fsm.Event(isNotInitializedEvent);
        }
    }
}
#endif

