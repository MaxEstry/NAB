#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Show self information")]
    public class DemoUtils_TurnBased_ShowSelfInfo : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(DemoUtils_TurnBasedManager))]
        [Tooltip("Object contain current match data")]
        public FsmGameObject turnBaseManager;

        [ActionSection("Result")]

        [Tooltip("Returned string")]
        public FsmString alertString;

        [Tooltip("Event sent if get the current match successful ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if get the current match not successful ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            turnBaseManager = null;
            alertString = null;
        }

        public override void OnEnter()
        {
            DemoUtils_TurnBasedManager manager = turnBaseManager.Value.GetComponent<DemoUtils_TurnBasedManager>();
            TurnBasedMatch currentMatch = manager.CurrentMatch;
            if (currentMatch == null)
            {
                alertString.Value = "Please create a match first.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }
            else
            {
                alertString.Value = manager.GetParticipantDisplayString(currentMatch.Self);
                Fsm.Event(eventTarget, isSuccessEvent);
            }
        }
    }
}

#endif