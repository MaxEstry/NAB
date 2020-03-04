#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Returns True whenever the Giphy API is in use. Use this to display Giphy attribution mark appropriately.")]
    public class Gif_IsUsingGiphyApi : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if Giphy API is in use, False otherwise.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isUsing;

        [Tooltip("Event sent if Giphy API is in use.")]
        public FsmEvent isUsingEvent;

        [Tooltip("Event sent if Giphy API is not in use.")]
        public FsmEvent isNotUsingEvent;

        public override void Reset()
        {
            everyFrame = false;
            isUsing = null;
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
            isUsing.Value = Giphy.IsUsingAPI;

            if (isUsing.Value)
                Fsm.Event(isUsingEvent);
            else
                Fsm.Event(isNotUsingEvent);
        }
    }
}

#endif
