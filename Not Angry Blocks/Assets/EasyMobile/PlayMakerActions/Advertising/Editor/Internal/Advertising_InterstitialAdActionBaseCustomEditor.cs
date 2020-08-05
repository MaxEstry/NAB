#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    public class Advertising_InterstitialAdActionBaseCustomEditor : CustomActionEditor
    {
        Advertising_InterstitialAdActionBase _action;

        public override bool OnGUI()
        {
            return false;
        }

        public bool EditActionParamField()
        {
            _action = (Advertising_InterstitialAdActionBase)target;

            EditField("param");

            switch (_action.param)
            {
                case Advertising_InterstitialAdActionBase.InterstitialAdActionParam.Default:
                    break;
                case Advertising_InterstitialAdActionBase.InterstitialAdActionParam.AdNetwork_AdPlacement:
                    EditField("adNetwork");
                    EditField("adPlacement");
                    break;
            }

            return GUI.changed;
        }
    }
}
#endif