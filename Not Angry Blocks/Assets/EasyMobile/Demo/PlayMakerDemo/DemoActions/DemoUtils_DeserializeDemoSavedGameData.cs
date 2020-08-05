#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using DemoSavedGameData = EasyMobile.Demo.GameServicesDemo_SavedGames.DemoSavedGameData;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Takes a base64 data string, converts it to a DemoSavedGameData object and returns the demo int value.")]
    public class DemoUtils_DeserializeDemoSavedGameData : FsmStateAction
    {
        [RequiredField]
        public FsmString base64Data;

        [ActionSection("Result")]

        [Tooltip("The parsed value.")]
        [UIHint(UIHint.Variable)]
        public FsmInt demoValue;

        public override void Reset()
        {
            base64Data = null;
            demoValue = 0;
        }

        public override void OnEnter()
        {
            if (base64Data != null && !string.IsNullOrEmpty(base64Data.Value))
            {
                // First convert base64 back to byte[]
                byte[] binaryData = System.Convert.FromBase64String(base64Data.Value);

                // Next convert byte[] to our custom saved game data class
                DemoSavedGameData dataObject = ByteArrayToSavedGameData(binaryData);

                if (dataObject != null)
                    demoValue.Value = dataObject.demoInt;
                else
                    Debug.Log("Error constructing demo saved game data object.");
            }
            else
            {
                Debug.Log("Nothing to parse.");
            }

            Finish();
        }

        DemoSavedGameData ByteArrayToSavedGameData(byte[] data)
        {
            if (data != null)
            {
                // Byte[] data to json string
                string jsonStr = System.Text.Encoding.UTF8.GetString(data);

                // Json string to object
                DemoSavedGameData savedData = JsonUtility.FromJson<DemoSavedGameData>(jsonStr);

                return savedData;
            }

            return null;
        }
    }
}
#endif

