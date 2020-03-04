#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Enables/Disables auto loading of default ads.")]
    public class Advertising_SetAutoAdLoadingMode : FsmStateAction
    {
        [Tooltip("The auto ad loading mode to use.")]
        [ObjectType(typeof(AutoAdLoadingMode))]
        public FsmEnum adLoadingMode;

        public override void OnEnter()
        {
            Advertising.AutoAdLoadingMode = (AutoAdLoadingMode)adLoadingMode.Value;
            Finish();
        }
    }
}
#endif
