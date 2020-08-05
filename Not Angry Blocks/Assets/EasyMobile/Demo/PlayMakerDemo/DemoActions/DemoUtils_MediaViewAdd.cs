#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions.Internal
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Add a media view")]
    public class DemoUtils_MediaViewAdd: FsmStateAction
    {
        [Tooltip("The Media DemoUtil")]
        public FsmGameObject demoUtilObj;

        [Tooltip("Mediasult")]
        public FsmObject mediaResultObj;

        public override void Reset()
        {
            base.Reset();           
            demoUtilObj = null;
            mediaResultObj = null;
        }

        public override void OnEnter()
        {
            MediaResultObject temp = (MediaResultObject)mediaResultObj.Value;

            demoUtilObj.Value.GetComponent<DemoUtils_Media>().AddView(temp.Media);

            Finish();

        }
    }
}

#endif