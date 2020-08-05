#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions.Internal
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Update display image.")]
    public class DemoUtils_MediaUpdateImage: FsmStateAction
    {
        [Tooltip("The media DemoUtil")]
        public FsmGameObject demoUtilObj;

        [Tooltip("Image")]
        public FsmTexture image;

        public override void Reset()
        {
            base.Reset();           
            demoUtilObj = null;
            image = null;
        }

        public override void OnEnter()
        {
            Texture2D image2D = (Texture2D)image.Value;

            demoUtilObj.Value.GetComponent<DemoUtils_Media>().UpdateDisplayImage(image2D);

            Finish();

        }
    }
}

#endif