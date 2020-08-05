#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using System.Text;
using System.Collections.Generic;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Creates a quick match with match request")]
    public class GameServices_RealTime_EventListener : FsmStateAction
    {
        [Tooltip("The current gameobject")]
        public FsmOwnerDefault gameObject;

        [ActionSection("Should reinvite player")]
        [Tooltip("Checks if we should reinvite player when disconnected")]
        public FsmBool shouldReinvite;

        [ActionSection("Result")]

        [Tooltip("The room setup progress in percent (0.0 to 100.0).")]
        public FsmFloat percent;

        [Tooltip("Notifies that room setup is finished. If <c>success == true</c>, you should " +
        "react by starting to play the game; otherwise, show an error screen.")]
        public FsmEvent onRoomConnectedEvent;

        [Tooltip("Event sent if room is not connected ")]
        public FsmEvent onRoomNotConnectedEvent;

        [Tooltip("Notifies that the current player has left the room. This may have happened " +
        "because you called LeaveRoom, or because an error occurred and the player " +
        "was dropped from the room.")]
        public FsmEvent onLeftRoomEvent;

        [Tooltip("This is called during room setup if a player declines an invitation or leaves.")]
        public FsmEvent onParticipantLeftEvent;

        [Tooltip("The participant that left.")]
        public FsmObject participantLeft;

        [Tooltip("Called when peers connect to the room.")]
        public FsmEvent onPeersConnectedEvent;

        [Tooltip("Called when peers disconnect from the room.")]
        public FsmEvent onPeersDisconnectedEvent;

        [Tooltip("Connected participant identifiers.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray connectedPeersId;

        [Tooltip("Disconnected participant identifiers.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray disconnectedPeersId;

        [Tooltip("Called when a real-time message is received.")]
        public FsmEvent onRealTimeMessageReceivedEvent;

        [Tooltip("sender ID.")]
        public FsmString senderId;

        [Tooltip("Message Data. The data to send in form of a base64 string.")]
        [UIHint(UIHint.Variable)]
        public FsmString msgData;

        [Tooltip(" [Game Center only] Called when a player in a two-player match was disconnected.")]
        public FsmEvent shouldReinviteDisconnectedPlayerEvent;

        [Tooltip(" [Game Center only] Called when a player in a two-player match was disconnected.")]
        public FsmEvent shouldNotReinviteDisconnectedPlayerEvent;

        [Tooltip("The player should we invite ")]
        public FsmObject playerShouldInvite;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            shouldReinvite = false;
            percent = 0;
            participantLeft = null;
            connectedPeersId = null;
            disconnectedPeersId = null;
            senderId = null;
            msgData = null;
            playerShouldInvite = null;
        }

        public override void OnEnter()
        {
            GameObject eventListenerObject =  gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;
            InternalListener internalListener = eventListenerObject.AddComponent<InternalListener>();
            internalListener.mainAction = this;
        }
        public override void OnExit()
        {

        }

        private class InternalListener: MonoBehaviour, IRealTimeMultiplayerListener
        {
            public GameServices_RealTime_EventListener mainAction;
            
            public void OnLeftRoom()
            {
                mainAction.Fsm.Event(mainAction.eventTarget, mainAction.onLeftRoomEvent);
            }

            public void OnParticipantLeft(Participant participant)
            {
                ParticipantObject temp = new ParticipantObject();
                temp.ParticipantData = participant;
                if(participant.Player != null)
                {
                    temp.Image = participant.Player.image;
                }
                mainAction.participantLeft.Value = temp;
                mainAction.Fsm.Event(mainAction.eventTarget, mainAction.onParticipantLeftEvent);
            }

            public void OnPeersConnected(string[] participantIds)
            {
                if (participantIds != null)
                {
                    mainAction.connectedPeersId.Resize(participantIds.Length);
                    for (int i = 0; i < participantIds.Length; i++)
                    {
                        mainAction.connectedPeersId.Set(i, participantIds[i]);
                    }
                    mainAction.Fsm.Event(mainAction.eventTarget, mainAction.onPeersConnectedEvent);
                }
            }

            public void OnPeersDisconnected(string[] participantIds)
            {
                if ((participantIds != null) && (participantIds.Length > 0))
                {
                    mainAction.disconnectedPeersId.Resize(participantIds.Length);
                    for (int i = 0; i < participantIds.Length; i++)
                    {
                        mainAction.disconnectedPeersId.Set(i, participantIds[i]);
                    }
                    mainAction.Fsm.Event(mainAction.eventTarget, mainAction.onPeersDisconnectedEvent);
                }
            }

            public void OnRealTimeMessageReceived(string senderId2, byte[] data)
            {
                mainAction.senderId.Value = senderId2;
                mainAction.msgData.Value = Convert.ToBase64String(data);

                mainAction.Fsm.Event(mainAction.eventTarget, mainAction.onRealTimeMessageReceivedEvent);
            }

            public void OnRoomConnected(bool success)
            {
                Debug.Log("[OnRoomSetupConnected]: " + success);
                if (success)
                {
                    mainAction.Fsm.Event(mainAction.eventTarget, mainAction.onRoomConnectedEvent);
                }
                else
                {
                    mainAction.Fsm.Event(mainAction.eventTarget, mainAction.onRoomNotConnectedEvent);
                }
            }

            public void OnRoomSetupProgress(float percent2)
            {
                Debug.Log("[OnRoomSetupProgress]. Percent: " + percent2);
                mainAction.percent.Value = percent2;
            }

            public bool ShouldReinviteDisconnectedPlayer(Participant participant)
            {
                if (mainAction.shouldReinvite.Value)
                {
                    ParticipantObject temp = new ParticipantObject();
                    temp.ParticipantData = participant;
                    if (participant.Player != null)
                        temp.Image = participant.Player.image;
                    mainAction.playerShouldInvite.Value = temp;
                    mainAction.Fsm.Event(mainAction.eventTarget, mainAction.shouldReinviteDisconnectedPlayerEvent);
                    return true;
                }
                else
                {
                    mainAction.Fsm.Event(mainAction.eventTarget, mainAction.shouldNotReinviteDisconnectedPlayerEvent);
                    return false;
                }
            }
        }
    }
}

#endif