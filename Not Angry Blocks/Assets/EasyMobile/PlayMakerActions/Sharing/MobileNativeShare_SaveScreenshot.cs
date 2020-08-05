#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Sharing")]
    [Tooltip("Captures a screenshot and saves it as a PNG image with the given filename." +
        "The created file is saved to the persistentDataPath if running on mobile devices," +
        "or to Assets folder if running in the editor.")]
    public class MobileNativeShare_SaveScreenshot : MobileNativeShare_ScreenshotCaptureActionBase
    {
        [Tooltip("Filename to store the resulted PNG image without the file extension.")]
        [RequiredField]
        public FsmString filename;

        [ActionSection("Result")]

        [Tooltip("The filepath of the saved screenshot.")]
        [UIHint(UIHint.Variable)]
        public FsmString filepath;

        private IEnumerator captureCoroutine;

        public override void Reset()
        {
            base.Reset();
            filename = null;
            filepath = null;
        }

        public override void OnEnter()
        {
            captureCoroutine = CRSaveScreenshot();
            RuntimeHelper.RunCoroutine(captureCoroutine);
        }

        public override void OnExit()
        {
            if (captureCoroutine != null)
                RuntimeHelper.EndCoroutine(captureCoroutine);
        }

        IEnumerator CRSaveScreenshot()
        {
            yield return new WaitForEndOfFrame();

            if (captureArea == ScreenshotCaptureOptions.WholeScreen)
                filepath.Value = Sharing.SaveScreenshot(filename.Value);
            else
                filepath.Value = Sharing.SaveScreenshot(startX.Value, startY.Value, width.Value, height.Value, filename.Value);

            captureCoroutine = null;
            Finish();
        }
    }
}

#endif
