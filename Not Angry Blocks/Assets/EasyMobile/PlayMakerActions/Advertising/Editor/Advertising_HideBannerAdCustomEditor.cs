#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Advertising_HideBannerAd))]
    public class Advertising_HideBannerAdCustomEditor : CustomActionEditor
    {
        Advertising_HideBannerAd _action;

        public override bool OnGUI()
        {
            _action = (Advertising_HideBannerAd)target;

            EditField("hideDefaultBannerAd");

            if (!_action.hideDefaultBannerAd.Value)
                EditField("adNetwork");
        
            return GUI.changed;
        }
    }
}
#endif