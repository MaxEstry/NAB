#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Advertising_IsRewardedAdReady))]
    public class Advertising_IsRewardedAdReadyCustomEditor : Advertising_RewardedAdActionBaseCustomEditor
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