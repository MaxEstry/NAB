#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Show detailed infomation of the next participant")]
    public class DemoUtils_TurnBased_ShowNextParticipantDetails : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(DemoUtils_TurnBasedManager))]
        [Tooltip("Object contain current match data")]
        public FsmGameObject turnBaseManager;

        [ActionSection("Result")]

        [Tooltip("Returned string")]
        public FsmString alertString;

        [Tooltip("Event sent get opponents info successful ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if there is no current match ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            turnBaseManager = null;
        }

        public override void OnEnter()
        {
            DemoUtils_TurnBasedManager manager = turnBaseManager.Value.GetComponent<DemoUtils_TurnBasedManager>();

            var nextParticipant = manager.SelectedParticipant;
            if (nextParticipant == null)
            {
                alertString.Value = "There's no infomations to show.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }

            alertString.Value = manager.GetParticipantDisplayString(nextParticipant);
            Fsm.Event(eventTarget, isSuccessEvent);

        }
        public override void OnExit()
        {

        }

    }
}

#endif