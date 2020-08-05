#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;
using System.Collections.Generic;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(GameService_LoadScores))]
    public class GameService_LoadScoresCustomEditor : CustomActionEditor
    {
        GameService_LoadScores _action;

        public override void OnEnable()
        {
            _action = (GameService_LoadScores)target;
        }

        public override bool OnGUI()
        {
            EditField("leaderboardName");
            EditField("scoreRange");

            if (_action.scoreRange == GameService_LoadScores.ScoreRangeOption.Custom)
            {
                EditField("fromRank");
                EditField("scoreCount");
                EditField("timeScope");
                EditField("userScope");
            }

            // Results
            EditField("loadedScoreCount");
            EditField("scores");
            EditField("ranks");
            EditField("eventTarget");
            EditField("scoresLoadedEvent");

            return GUI.changed;
        }
    }
}
#endif