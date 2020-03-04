#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Determines whether ads were removed permanently.")]
    public class Advertising_IsAdRemoved : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if ads were removed permanently, False otherwise.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isRemoved;

        [Tooltip("Event sent if ads were removed.")]
        public FsmEvent isRemovedEvent;

        [Tooltip("Event sent if ads have not been removed.")]
        public FsmEvent isNotRemovedEvent;

        public override void Reset()
        {
            isRemoved = null;
            everyFrame = false;
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
            isRemoved.Value = Advertising.IsAdRemoved();

            if (isRemoved.Value)
                Fsm.Event(isRemovedEvent);
            else
                Fsm.Event(isNotRemovedEvent);
        }
    }
}
#endif

