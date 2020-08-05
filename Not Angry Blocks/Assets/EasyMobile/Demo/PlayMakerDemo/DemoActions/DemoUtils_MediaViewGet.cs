#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions.Internal
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Get MediaResult from a media view")]
    public class DemoUtils_MediaViewGet: FsmStateAction
    {
        [Tooltip("Media View Prefab")]
        public FsmGameObject mediaView;

        [ActionSection("Result")]

        [Tooltip("Mediasult")]
        public FsmObject mediaResultObj;

        public override void Reset()
        {
            base.Reset();           
            mediaView = null;
            mediaResultObj = null;
        }

        public override void OnEnter()
        {
            MediaResultObject temp = new MediaResultObject();

            temp.Media = mediaView.Value.GetComponent<MediaResultView>().GetMedia();

            mediaResultObj.Value = temp;

            Finish();

        }
    }
}

#endif