#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Stop Spinning")]
    public class DemoUtils_RealTime_StopSpinning : FsmStateAction
    {
        [Tooltip("Demo RealTime manager object")]
        public FsmGameObject managerObj;

        public override void Reset()
        {
            base.Reset();
            managerObj = null;
        }

        public override void OnEnter()
        {
            DemoUtils_RealTime_Manager manager = managerObj.Value.GetComponent<DemoUtils_RealTime_Manager>();

            manager.StopSpinning();
            manager.ClearReceivedMessagesText();
            Finish();
        }
    }
}

#endif