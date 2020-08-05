#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Handle Received Message")]
    public class DemoUtils_RealTime_ShowHideGameUI : FsmStateAction
    {
        [Tooltip("check true if user want to show or hide")]
        public FsmBool isShow;


        [Tooltip("The match request root")]
        public FsmGameObject matchRequestRoot;

        [Tooltip("The match request root")]
        public FsmGameObject matchCreationRoot;

        [Tooltip("The match request root")]
        public FsmGameObject ingameRoot;



        public override void Reset()
        {
            base.Reset();
        }

        public override void OnEnter()
        {
            if (isShow.Value)
            {
                ShowInGameUI();
            }
            else
            {
                ShowSetupUI();
            }
            Finish();
        }

        private void ShowSetupUI()
        {
            matchRequestRoot.Value.SetActive(true);
            matchCreationRoot.Value.SetActive(true);
            ingameRoot.Value.SetActive(false);
        }

        private void ShowInGameUI()
        {
            matchRequestRoot.Value.SetActive(false);
            matchCreationRoot.Value.SetActive(false);
            ingameRoot.Value.SetActive(true);
        }

    }
}

#endif