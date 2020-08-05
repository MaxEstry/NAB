#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Advertising_IsInterstitialAdReady))]
    public class Advertising_IsInterstitialAdReadyCustomEditor : Advertising_InterstitialAdActionBaseCustomEditor
    {
        public override bool OnGUI()
        {
            bool changed = EditActionParamField();

            EditField("everyFrame");

            // Results
            EditField("isReady");
            EditField("isReadyEvent");
            EditField("isNotReadyEvent");

            return changed || GUI.changed;
        }
    }
}
#endif