#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Core")]
    [Tooltip("Initializes the Easy Mobile runtime. This is required before any other Easy Mobile actions can be used.")]
    public class RuntimeManager_Init : FsmStateAction
    {
        public override void OnEnter()
        {
            RuntimeManager.Init();
            Finish();
        }
    }
}
#endif

