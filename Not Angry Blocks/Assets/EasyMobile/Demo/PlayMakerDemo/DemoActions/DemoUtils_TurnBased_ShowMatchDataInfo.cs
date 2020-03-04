#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Create a quick match with match request")]
    public class DemoUtils_TurnBased_ShowMatchDataInfo : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(DemoUtils_TurnBasedManager))]
        [Tooltip("Object contain current match data")]
        public FsmGameObject turnBaseManager;

        [ActionSection("Result")]

        [Tooltip("Match Data. The data to send in form of a base64 string.")]
        public FsmString matchData;

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
            matchData = null;
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

            if (manager.CurrentMatchData == null)
            {
                alertString.Value = "The current match doesn't have any data to show.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }

            var bytes = manager.CurrentMatchData.ToByteArray();
            string dataSize = bytes != null ? (bytes.Length.ToString() + " byte(s)") : "Error";
            alertString.Value = string.Format("Turn Count: {0}\nWinner Name: {1}\nSize: {2}",
                                 manager.CurrentMatchData.TurnCount, manager.CurrentMatchData.WinnerName, dataSize);

            matchData.Value = Convert.ToBase64String(bytes);
            Fsm.Event(eventTarget, isSuccessEvent);

        }
        public override void OnExit()
        {

        }

    }
}

#endif