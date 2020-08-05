#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System;
using EasyMobile.Demo;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Handle Received Message")]
    public class DemoUtils_RealTime_HandleReceivedMessage : FsmStateAction
    {

        [Tooltip("Sender ID")]
        public FsmString senderId;

        [Tooltip("Message Data. The data to send in form of a base64 string.")]
        public FsmString msgData;

        [Tooltip("Event Listener")]
        public FsmGameObject listenerObj;


        [ActionSection("Result")]

        [Tooltip("Output Message String.")]
        public FsmString outputString;

        public override void Reset()
        {
            base.Reset();
            msgData = null;
            listenerObj = null;
        }

        public override void OnEnter()
        {
            byte[] data = Convert.FromBase64String(msgData.Value);

            DemoUtils_RealTime_Manager listener = listenerObj.Value.GetComponent<DemoUtils_RealTime_Manager>();

            try
            {
                var sampleData = GameServicesDemo_Multiplayer_RealtimeKitchenSink.SampleData.FromByteArray(data);
                listener.AddReceivedMessage("SenderId: " + senderId.Value + "\nData: " + (sampleData != null ? "\n" + sampleData.ToString() : "null"));
            }
            catch (Exception e)
            {
                listener.AddReceivedMessage("[Error]. Error orcurs when trying to parse data: \n" + e.Message + "\n" + e.StackTrace);
            }
            Finish();
        }




    }
}

#endif