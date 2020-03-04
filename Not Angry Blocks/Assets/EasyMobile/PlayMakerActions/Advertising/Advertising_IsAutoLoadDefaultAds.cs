#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Gets auto-load default ads settings. Returns True if auto-load ads is enabled.")]
    public class Advertising_IsAutoLoadDefaultAds : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if auto loading default ads is enabled, False otherwise.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isAutoLoad;

        [Tooltip("Event sent if auto loading default ads is enabled.")]
        public FsmEvent isAutoLoadEvent;

        [Tooltip("Event sent if auto loading default ads is disabled.")]
        public FsmEvent isNotAutoLoadEvent;

        public override void Reset()
        {
            isAutoLoad = null;
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

#pragma warning disable 0618
        void DoMyAction()
        {
            isAutoLoad.Value = Advertising.IsAutoLoadDefaultAds();

            if (isAutoLoad.Value)
                Fsm.Event(isAutoLoadEvent);
            else
                Fsm.Event(isNotAutoLoadEvent);
        }
    }
#pragma warning restore 0618
}
#endif