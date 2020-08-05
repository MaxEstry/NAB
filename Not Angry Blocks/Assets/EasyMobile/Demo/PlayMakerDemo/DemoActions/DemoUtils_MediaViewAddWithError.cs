#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions.Internal
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Add a media view with error")]
    public class DemoUtils_MediaViewAddWithError: FsmStateAction
    {
        [Tooltip("Media DemoUtil")]
        public FsmGameObject demoUtilObj;

        [Tooltip("ErrorMSg")]
        public FsmString error;

        public override void Reset()
        {
            base.Reset();           
            error = null;
            demoUtilObj = null;
        }

        public override void OnEnter()
        {
            demoUtilObj.Value.GetComponent<DemoUtils_Media>().AddViewWithError(error.Value);

            Finish();

        }
    }
}

#endif