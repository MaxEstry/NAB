#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Create a quick match with match request")]
    public class DemoUtils_GetMessageData : FsmStateAction
    {
        [Tooltip("participant name")]
        public FsmString displayName;

        [Tooltip("Message's text")]
        public FsmString msgText;

        [Tooltip("Message's data")]
        public FsmString msgData;

        [Tooltip("Dummy Size")]
        public FsmString dummySizeIn;

        [ActionSection("Result")]

        [Tooltip("Check if we should reinvite player when disconnected")]
        public FsmString participantId;

        [Tooltip("Output Message after converted")]
        public FsmString outputMsgData;


        public override void Reset()
        {
            base.Reset();
            participantId = null;
        }

        public override void OnEnter()
        {
            Participant[] participants = GameServices.RealTime.GetConnectedParticipants().ToArray();
            if (displayName != null)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].DisplayName == displayName.Value)
                    {
                        participantId.Value = participants[i].ParticipantId;
                        break;
                    }
                }
            }
            var data = GetSentData();
            if (data != null)
            {
                outputMsgData.Value = Convert.ToBase64String(data);
            }
            Finish();
        }



        private byte[] GetSentData()
        {
            float value = 0;
            if (!float.TryParse(msgData.Value, out value))
                return null;

            var sampleData = new GameServicesDemo_Multiplayer_RealtimeKitchenSink.SampleData()
            {
                TimeStamp = DateTime.UtcNow,
                Text = msgText.Value,
                Value = value,
            };

            int dummySize = 0;
            if (int.TryParse(dummySizeIn.Value, out dummySize))
                sampleData.UpdateDummySize(dummySize);

            return sampleData.ToByteArray();

        }
    }
}

#endif