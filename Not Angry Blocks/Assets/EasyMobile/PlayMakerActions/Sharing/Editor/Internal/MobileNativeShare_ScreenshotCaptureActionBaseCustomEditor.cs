#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;
using System;

namespace EasyMobile.PlayerMaker.Editor
{
    public class MobileNativeShare_ScreenshotCaptureActionBaseCustomEditor : CustomActionEditor
    {
        MobileNativeShare_ScreenshotCaptureActionBase _action;

        public override bool OnGUI()
        {
            return false;
        }

        protected bool EditCaptureAreaField()
        {
            _action = (MobileNativeShare_ScreenshotCaptureActionBase)target;

            EditField("captureArea");

            switch (_action.captureArea)
            {
                case MobileNativeShare_ScreenshotCaptureActionBase.ScreenshotCaptureOptions.WholeScreen:
                    break;
                case MobileNativeShare_ScreenshotCaptureActionBase.ScreenshotCaptureOptions.SpecifiedArea:
                    EditField("startX");
                    EditField("startY");
                    EditField("width");
                    EditField("height");
                    break;
            }

            return GUI.changed;
        }
    }
}
#endif