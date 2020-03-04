#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Advertising_DestroyBannerAd))]
    public class Advertising_DestroyBannerAdCustomEditor : CustomActionEditor
    {
        Advertising_DestroyBannerAd _action;

        public override bool OnGUI()
        {
            _action = (Advertising_DestroyBannerAd)target;

            EditField("destroyDefaultBannerAd");

            if (!_action.destroyDefaultBannerAd.Value)
                EditField("adNetwork");
        
            return GUI.changed;
        }
    }
}
#endif