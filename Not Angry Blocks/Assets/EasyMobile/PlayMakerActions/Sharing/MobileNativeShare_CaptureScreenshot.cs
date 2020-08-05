#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Sharing")]
    [Tooltip("Captures a screenshot into a Texture2D object and returns it.")]
    public class MobileNativeShare_CaptureScreenshot : MobileNativeShare_ScreenshotCaptureActionBase
    {
        [ActionSection("Result")]

        [Tooltip("The resulted Texture2D object.")]
        public FsmTexture screenshot;

        private IEnumerator captureCoroutine;

        public override void Reset()
        {
            base.Reset();
            screenshot = null;
        }

        public override void OnEnter()
        {
            captureCoroutine = CRCaptureScreenshot();
            RuntimeHelper.RunCoroutine(captureCoroutine);
        }

        public override void OnExit()
        {
            if (captureCoroutine != null)
                RuntimeHelper.EndCoroutine(captureCoroutine);
        }

        IEnumerator CRCaptureScreenshot()
        {
            yield return new WaitForEndOfFrame();

            if (captureArea == ScreenshotCaptureOptions.WholeScreen)
                screenshot.Value = Sharing.CaptureScreenshot();
            else
                screenshot.Value = Sharing.CaptureScreenshot(startX.Value, startY.Value, width.Value, height.Value);

            captureCoroutine = null;
            Finish();
        }
    }
}

#endif
