#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using DemoSavedGameData = EasyMobile.Demo.GameServicesDemo_SavedGames.DemoSavedGameData;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Takes a demo int value, constructs a DemoSavedGameData object with it and then converts the object to " +
        "a byte array and returns a base64 representation of it.")]
    public class DemoUtils_SerializeDemoSavedGameData : FsmStateAction
    {
        [RequiredField]
        public FsmInt demoValue;

        [ActionSection("Result")]

        [Tooltip("The base64 string representation of the constructed DemoSavedGameData.")]
        [UIHint(UIHint.Variable)]
        public FsmString base64Data;

        public override void Reset()
        {
            demoValue = 0;
            base64Data = null;
        }

        public override void OnEnter()
        {
            // Create a new object from the demo value and convert it to byte[]
            byte[] data = SavedGameDataToByteArray(new DemoSavedGameData(demoValue.Value));

            // Convert byte[] to base64 string and return.
            base64Data.Value = System.Convert.ToBase64String(data);

            Finish();
        }

        byte[] SavedGameDataToByteArray(DemoSavedGameData dataObj)
        {
            if (dataObj != null)
            {
                // Convert to json string
                string jsonStr = JsonUtility.ToJson(dataObj);

                // Json string to byte[]
                return System.Text.Encoding.UTF8.GetBytes(jsonStr);
            }

            return null;
        }
    }
}
#endif

