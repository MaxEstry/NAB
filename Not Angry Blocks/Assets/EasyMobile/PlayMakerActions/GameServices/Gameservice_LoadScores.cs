#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.SocialPlatforms;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Loads a set of scores from the specified leaderboard. " +
        "The 'Scores Loaded Event' is sent when the scores are loaded successfully. " +
        "The action finishes either when the scores are loaded, or the loading " +
        "fails due to the service not being initialized, or the leaderboard name is incorrect.")]
    public class GameService_LoadScores : FsmStateAction
    {
        public enum ScoreRangeOption
        {
            Default,
            Custom
        }

        [Tooltip("Name of the leaderboard to load scores from.")]
        [RequiredField]
        public FsmString leaderboardName;

        [Tooltip("Whether to load the default or custom range of scores." +
            "\nSet to 'Default' to load 25 scores that are around the " +
            "local player's score in the 'Global' userScope and 'AllTime' timeScope." +
            "\nOtherwise set to 'Custom' to load a custom set of scores.")]
        public ScoreRangeOption scoreRange;

        [Tooltip("The rank of the first score to load.")]
        public FsmInt fromRank;

        [Tooltip("The total number of the scores to load.")]
        public FsmInt scoreCount;

        [Tooltip("The time scope of the scores to load.")]
        [ObjectType(typeof(TimeScope))]
        public FsmEnum timeScope;

        [Tooltip("The user scope of the scores to load.")]
        [ObjectType(typeof(UserScope))]
        public FsmEnum userScope;

        [ActionSection("Result")]

        [Tooltip("The number of loaded scores.")]
        [UIHint(UIHint.Variable)]
        public FsmInt loadedScoreCount;

        [Tooltip("The array of the loaded scores. The associated rank of each score " +
            "has the same index in the 'Ranks' array.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Int)]
        public FsmArray scores;

        [Tooltip("The array of ranks of the loaded scores. The associated score of each rank " +
            "has the same index in the 'Scores' array.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Int)]
        public FsmArray ranks;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the scores are loaded.")]
        public FsmEvent scoresLoadedEvent;

        public override void Reset()
        {
            leaderboardName = null;
            loadedScoreCount = null;
            scores = null;
            ranks = null;
            eventTarget = null;
            scoresLoadedEvent = null;
        }

        public override void OnEnter()
        {
            if (GameServices.IsInitialized() &&
                !string.IsNullOrEmpty(leaderboardName.Value) &&
                GameServices.GetLeaderboardByName(leaderboardName.Value) != null)
            {
                if (scoreRange == ScoreRangeOption.Default)
                    GameServices.LoadScores(leaderboardName.Value, ScoresLoadedCallback);
                else
                    GameServices.LoadScores(
                        leaderboardName.Value, 
                        fromRank.Value, 
                        scoreCount.Value, 
                        (TimeScope)timeScope.Value, 
                        (UserScope)userScope.Value,
                        ScoresLoadedCallback
                    );
            }
            else
            {
                Finish();
            }
        }

        void ScoresLoadedCallback(string ldbName, IScore[] loadedScores)
        {
            loadedScoreCount.Value = loadedScores.Length;

            for (int i = 0; i < loadedScoreCount.Value; i++)
            {
                scores.Values[i] = (int)loadedScores[i].value;
                ranks.Values[i] = loadedScores[i].rank;
            }
                
            Fsm.Event(eventTarget, scoresLoadedEvent);
            Finish();
        }
    }
}
#endif