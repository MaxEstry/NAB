#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Advertising_ShowBannerAd))]
    public class Advertising_ShowBannerAdCustomEditor : CustomActionEditor
    {
        Advertising_ShowBannerAd _action;

        public override bool OnGUI()
        {
            _action = (Advertising_ShowBannerAd)target;

            EditField("param");

            switch (_action.param)
            {
                case Advertising_ShowBannerAd.ShowBannerAdParam.Position_Size_AdNetwork:
                    EditField("adNetwork");
                    goto case Advertising_ShowBannerAd.ShowBannerAdParam.Position_Size;
                case Advertising_ShowBannerAd.ShowBannerAdParam.Position_Size:
                    EditField("useDefaultBannerSize");
                    if (!_action.useDefaultBannerSize.Value)
                    {
                        EditField("bannerWidth");
                        EditField("bannerHeight");
                    }
                    goto case Advertising_ShowBannerAd.ShowBannerAdParam.Position;
                case Advertising_ShowBannerAd.ShowBannerAdParam.Position:
                    EditField("bannerPosition");
                    break;
            }
            return GUI.changed;
        }
    }
}
#endif