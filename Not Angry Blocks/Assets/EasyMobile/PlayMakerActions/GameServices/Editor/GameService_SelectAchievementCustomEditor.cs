#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;
using System.Collections.Generic;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(GameService_SelectAchievement))]
    public class GameService_SelectAchievementCustomEditor : GameService_SelectItemBaseCustomEditor
    {
        GameService_SelectAchievement _action;

        public override void OnEnable()
        {
            base.OnEnable();
            _action = (GameService_SelectAchievement)target;
        }

        public override bool OnGUI()
        {
            bool changed = EditSelectItemField("Achievement", _action.achievement);

            EditField("name");
            EditField("id");

            return changed || GUI.changed;
        }
    }
}
#endif