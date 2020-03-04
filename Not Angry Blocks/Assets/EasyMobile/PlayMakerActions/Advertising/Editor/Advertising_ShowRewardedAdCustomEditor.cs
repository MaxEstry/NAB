#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Advertising_ShowRewardedAd))]
    public class Advertising_ShowRewardedAdCustomEditor : Advertising_RewardedAdActionBaseCustomEditor
    {
        public override bool OnGUI()
        {
            bool changed = EditActionParamField();
            return changed || GUI.changed;
        }
    }
}
#endif