#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Utilities")]
    [Tooltip("Opens a URL.")]
    public class Misc_OpenUrl : FsmStateAction
    {
        [Tooltip("The URL to open.")]
        [RequiredField]
        public FsmString url;

        public override void Reset()
        {
            url = string.Empty;
        }

        public override void OnEnter()
        {
            Application.OpenURL(url.Value);
            Finish();
        }
    }
}
#endif