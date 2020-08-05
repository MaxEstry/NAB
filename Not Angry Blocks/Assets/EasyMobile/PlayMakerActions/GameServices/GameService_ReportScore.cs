# if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Reports the given score to the leaderboard with the specified name.")]
    public class GameService_ReportScore : FsmStateAction
    {
        [Tooltip("The leaderboard to report score to.")]
        public FsmString leaderboardName;

        [Tooltip("Score to report.")]
        public FsmInt score;

        [ActionSection("Result")]

        [Tooltip("True if the score was reported successfully, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Where to send the events.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent if the score was reported successfully.")]
        public FsmEvent successEvent;

        [Tooltip("Event sent if the score was failed to report.")]
        public FsmEvent failureEvent;

        public override void Reset()
        {
            score = null;
            leaderboardName = null;
            isSuccess = false;
            eventTarget = null;
            successEvent = null;
            failureEvent = null;
        }

        public override void OnEnter()
        {
            if (GameServices.IsInitialized() && GameServices.GetLeaderboardByName(leaderboardName.Value) != null)
            {
                GameServices.ReportScore(
                    score.Value, 
                    leaderboardName.Value, 
                    (bool success) =>
                    {
                        isSuccess.Value = success;

                        if (success)
                            Fsm.Event(eventTarget, successEvent);
                        else
                            Fsm.Event(eventTarget, failureEvent);

                        Finish();
                    });
            }
            else
            {
                isSuccess.Value = false;
                Fsm.Event(eventTarget, failureEvent);
                Finish();
            }
        }
    }
}
#endif