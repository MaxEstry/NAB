#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile;
using EasyMobile.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Sharing")]
    [Tooltip("Captures the screenshot, saves it as a PNG image to the persistentDataPath and then shares it" +
        "to social networks via the native sharing utility.")]
    public class MobileNativeShare_ShareScreenshot : MobileNativeShare_ScreenshotCaptureActionBase
    {
        [Tooltip("Filename to save the screenshot, no need the .png extension.")]
        [RequiredField]
        public FsmString filename;

        [Tooltip("[Optional] The sharing message.")]
        public FsmString message;

        [Tooltip("[Optional] The sharing subject.")]
        public FsmString subject;

        [ActionSection("Result")]

        [Tooltip("The file path of saved screenshot.")]
        [UIHint(UIHint.Variable)]
        public FsmString filepath;

        private IEnumerator captureCoroutine;

        public override void Reset()
        {
            base.Reset();
            filename = null;
            message = "";
            subject = "";
            filepath = null;
        }

        public override void OnEnter()
        {
            captureCoroutine = CRShareScreenshot();
            RuntimeHelper.RunCoroutine(captureCoroutine);
        }

        public override void OnExit()
        {
            if (captureCoroutine != null)
                RuntimeHelper.EndCoroutine(captureCoroutine);
        }

        IEnumerator CRShareScreenshot()
        {
            yield return new WaitForEndOfFrame();

            if (captureArea == ScreenshotCaptureOptions.WholeScreen)
                filepath.Value = Sharing.ShareScreenshot(filename.Value, message.Value, subject.Value);
            else
                filepath.Value = Sharing.ShareScreenshot(startX.Value, startY.Value, width.Value, height.Value, filename.Value, message.Value, subject.Value);

            captureCoroutine = null;
            Finish();
        }
    }
}

#endif
