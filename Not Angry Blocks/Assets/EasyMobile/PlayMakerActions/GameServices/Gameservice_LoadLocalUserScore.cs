#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.SocialPlatforms;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Loads the local user's score from the specified leaderboard. " +
        "The 'Score Loaded Event' is sent when the score is loaded successfully. " +
        "The action finishes either when the score is loaded, or the loading " +
        "fails due to the service not being initialized, or the leaderboard name is incorrect.")]
    public class GameService_LoadLocalUserScore : FsmStateAction
    {
        [Tooltip("Name of the leaderboard to load score from.")]
        [RequiredField]
        public FsmString leaderboardName;

        [ActionSection("Result")]

        [Tooltip("Local user's score loaded from the leaderboard.")]
        [UIHint(UIHint.Variable)]
        public FsmInt score;

        [Tooltip("The rank of the score on the leaderboard.")]
        [UIHint(UIHint.Variable)]
        public FsmInt rank;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the score is loaded.")]
        public FsmEvent scoreLoadedEvent;

        public override void Reset()
        {
            leaderboardName = null;
            score = null;
            rank = null;
            eventTarget = null;
            scoreLoadedEvent = null;
        }

        public override void OnEnter()
        {
            if (GameServices.IsInitialized() &&
                !string.IsNullOrEmpty(leaderboardName.Value) &&
                GameServices.GetLeaderboardByName(leaderboardName.Value) != null)
            {
                GameServices.LoadLocalUserScore(leaderboardName.Value, (ldbName, loadedScore) =>
                    {
                        score.Value = (int)loadedScore.value;
                        rank.Value = loadedScore.rank;
                        Fsm.Event(eventTarget, scoreLoadedEvent);
                        Finish();
                    });
            }
            else
            {
                Finish();
            }
        }
    }
}
#endif