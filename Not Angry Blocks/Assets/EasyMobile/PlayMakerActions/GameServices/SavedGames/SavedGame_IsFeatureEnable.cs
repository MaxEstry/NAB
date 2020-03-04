#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.SocialPlatforms;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Saved Games")]
    [Tooltip("Determines whether the Saved Games feature is enabled in Easy Mobile settings.")]
    public class SavedGame_IsFeatureEnable : FsmStateAction
    {
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if Saved Games feature is enabled, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isEnable;

        [Tooltip("Event sent if Saved Games feature is enable.")]
        public FsmEvent isEnableEvent;

        [Tooltip("Event sent if Saved Games feature is not enable.")]
        public FsmEvent isNotEnableEvent;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            everyFrame = false;
            isEnable = null;
            isEnableEvent = null;
            isNotEnableEvent = null;
            eventTarget = null;
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
            isEnable.Value = EM_Settings.GameServices.IsSavedGamesEnabled;

            if (isEnable.Value)
                Fsm.Event(eventTarget, isEnableEvent);
            else
                Fsm.Event(eventTarget, isNotEnableEvent);
        }
    }
}
#endif

