#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    public class Advertising_RewardedAdActionBaseCustomEditor : CustomActionEditor
    {
        Advertising_RewardedAdActionBase _action;

        public override bool OnGUI()
        {
            return false;
        }

        public bool EditActionParamField()
        {
            _action = (Advertising_RewardedAdActionBase)target;

            EditField("param");

            switch (_action.param)
            {
                case Advertising_RewardedAdActionBase.RewardedAdActionParam.Default:
                    break;
                case Advertising_RewardedAdActionBase.RewardedAdActionParam.AdNetwork_AdPlacement:
                    EditField("adNetwork");
                    EditField("adPlacement");
                    break;
            }

            return GUI.changed;
        }
    }
}
#endif