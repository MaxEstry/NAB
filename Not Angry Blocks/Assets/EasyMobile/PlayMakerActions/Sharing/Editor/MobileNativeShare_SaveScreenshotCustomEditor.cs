#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;
using System;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(MobileNativeShare_SaveScreenshot))]
    public class MobileNativeShare_SaveScreenshotActionBaseCustomEditor : MobileNativeShare_ScreenshotCaptureActionBaseCustomEditor
    {
        public override bool OnGUI()
        {
            bool changed = EditCaptureAreaField();

            EditField("filename");
            EditField("filepath");

            return changed || GUI.changed;
        }
    }
}
#endif
