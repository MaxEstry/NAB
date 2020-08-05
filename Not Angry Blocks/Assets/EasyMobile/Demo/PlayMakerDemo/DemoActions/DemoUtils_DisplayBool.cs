#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    public class DemoUtils_DisplayBool : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(DemoUtils))]
        public FsmGameObject demoUtils;

        [RequiredField]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        public FsmBool state;

        [RequiredField]
        public FsmString message;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        private DemoUtils demoUtilsComp;
        private GameObject go;

        public override void Reset()
        {
            gameObject = null;
            state = false;
            message = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            if (demoUtils.Value != null)
                demoUtilsComp = demoUtils.Value.GetComponent<DemoUtils>();

            go = Fsm.GetOwnerDefaultTarget(gameObject);

            DoMyAction();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoMyAction();
        }

        void DoMyAction()
        {
            if (demoUtilsComp != null && go != null)
                demoUtilsComp.DisplayBool(go, state.Value, message.Value);
        }
    }
}
#endif

