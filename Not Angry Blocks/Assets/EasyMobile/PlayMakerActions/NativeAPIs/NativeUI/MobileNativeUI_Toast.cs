#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Native APIs")]
    [Tooltip("[Android Only] Shows a toast message.")]
    public class MobileNativeUI_Toast : FsmStateAction
    {
        [Tooltip("The toast message.")]
        public FsmString message;

        [Tooltip("Set to True to show a long toast, False to show a short toast.")]
        public FsmBool longToast;

        public override void Reset()
        {
            message = null;
            longToast = false;
        }

        public override void OnEnter()
        {
            #if UNITY_ANDROID
            NativeUI.ShowToast(message.Value, longToast.Value);
            #endif
            
            Finish();
        }
    }
}

#endif
