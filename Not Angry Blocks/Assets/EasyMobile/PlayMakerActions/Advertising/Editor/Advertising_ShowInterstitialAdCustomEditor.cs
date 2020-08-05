#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Advertising_ShowInterstitialAd))]
    public class Advertising_ShowInterstitialAdCustomEditor : Advertising_InterstitialAdActionBaseCustomEditor
    {
        public override bool OnGUI()
        {
            bool changed = EditActionParamField();
            return changed || GUI.changed;
        }
    }
}
#endif