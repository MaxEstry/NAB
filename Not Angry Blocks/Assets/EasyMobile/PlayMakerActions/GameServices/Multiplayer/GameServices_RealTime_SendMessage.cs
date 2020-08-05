#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Sends a message to a particular participant.")]
    public class GameServices_RealTime_SendMessage : FsmStateAction
    {
        public enum SendMessageParam
        {
            Data,
            Data_Offset_Length
        }

        [Tooltip("Send message parameter(s)")]
        public SendMessageParam param;

        [Tooltip("Unreliable messages are faster, but are not guaranteed to arrive and may arrive out of order.")]
        public FsmBool reliable;

        [Tooltip("Participant ID. The participant to whom the message will be sent.")]
        public FsmString participantId;

        [Tooltip("Message's data")]
        public FsmString msgData;

        [Tooltip("Offset. Offset of the data buffer where data starts.")]
        public FsmInt offset;

        [Tooltip("Length. Length of data (from offset).")]
        public FsmInt length;

        public override void Reset()
        {
            base.Reset();
            reliable = false;
            msgData = null;
            offset = 0;
            length = 0;
            participantId = null;
        }

        public override void OnEnter()
        {
            byte[] data = Convert.FromBase64String(msgData.Value);

            switch (param)
            {
                case SendMessageParam.Data:
                    GameServices.RealTime.SendMessage(reliable.Value, participantId.Value, data);
                    break;
                case SendMessageParam.Data_Offset_Length:
                    GameServices.RealTime.SendMessage(reliable.Value, participantId.Value, data, offset.Value, length.Value);
                    break;
            }

            Finish();
        }

    }
}

#endif