#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;
using System.Collections.Generic;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(GameService_SelectLeaderboard))]
    public class GameService_SelectLeaderboardCustomEditor : GameService_SelectItemBaseCustomEditor
    {
        GameService_SelectLeaderboard _action;

        public override void OnEnable()
        {
            base.OnEnable();
            _action = (GameService_SelectLeaderboard)target;
        }

        public override bool OnGUI()
        {
            bool changed = EditSelectItemField("Leaderboard", _action.leaderboard);

            EditField("name");
            EditField("id");

            return changed || GUI.changed;
        }
    }
}
#endif