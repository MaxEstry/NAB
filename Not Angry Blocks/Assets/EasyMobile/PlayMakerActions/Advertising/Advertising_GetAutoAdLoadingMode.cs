#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Gets auto-load default ads settings. Returns True if auto-load ads is enabled.")]
    public class Advertising_GetAutoAdLoadingMode : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The auto ad-loading mode.")]
        [UIHint(UIHint.Variable)]
        [ObjectType(typeof(AutoAdLoadingMode))]
        public FsmEnum adLoadingMode;

        public override void Reset()
        {
            adLoadingMode = null;
        }

        public override void OnEnter()
        {
            adLoadingMode.Value = EM_Settings.Advertising.AutoAdLoadingMode ;             
            Finish();
        }
    }
}
#endif