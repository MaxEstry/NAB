#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;


namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Returns whether or not the room is connected (ready to play).")]
    public class GameServices_RealTime_IsRoomConnected : FsmStateAction
    {

        [Tooltip("Repeats every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if the room is connected. False otherwise")]
        [UIHint(UIHint.Variable)]
        public FsmBool IsRoomConnected;

        [Tooltip("Event sent if room is connected.")]
        public FsmEvent isConnectedEvent;

        [Tooltip("Event sent if room is not connected.")]
        public FsmEvent isNotConnectedEvent;

        public override void Reset()
        {
            IsRoomConnected = null;
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
            IsRoomConnected.Value = GameServices.RealTime.IsRoomConnected();

            if (IsRoomConnected.Value)
                Fsm.Event(isConnectedEvent);
            else
                Fsm.Event(isNotConnectedEvent);
        }
    }
}

#endif