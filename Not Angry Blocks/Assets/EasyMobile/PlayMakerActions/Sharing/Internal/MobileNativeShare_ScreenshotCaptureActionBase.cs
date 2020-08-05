#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    public abstract class MobileNativeShare_ScreenshotCaptureActionBase : FsmStateAction
    {
        public enum ScreenshotCaptureOptions
        {
            WholeScreen,
            SpecifiedArea
        }

        [Tooltip("Specify the screen area to capture.")]
        public ScreenshotCaptureOptions captureArea;

        [Tooltip("Start x.")]
        public FsmFloat startX;

        [Tooltip("Start y.")]
        public FsmFloat startY;

        [Tooltip("Width.")]
        public FsmFloat width;

        [Tooltip("Height.")]
        public FsmFloat height;

        public override void Reset()
        {
            captureArea = ScreenshotCaptureOptions.WholeScreen;
            startX = 0;
            startY = 0;
            width = 100;
            height = 100;
        }
    }
}

#endif
