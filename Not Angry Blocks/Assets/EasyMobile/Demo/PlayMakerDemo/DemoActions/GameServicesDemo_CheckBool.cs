#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Check a bool value")]
    public class GameServicesDemo_CheckBool : FsmStateAction
    {
        [Tooltip("Event Listener")]
        public FsmGameObject target;

        [Tooltip("Bool value")]
        public FsmBool isCheck;

        [Tooltip("Event Listener")]
        public FsmGameObject demoUtils;

        [Tooltip("Text string")]
        public FsmString text;


        public override void OnEnter()
        {
            demoUtils.Value.GetComponent<DemoUtils>().DisplayBool(target.Value, isCheck.Value, text.Value);

            Finish();
        }
    }
}
#endif
