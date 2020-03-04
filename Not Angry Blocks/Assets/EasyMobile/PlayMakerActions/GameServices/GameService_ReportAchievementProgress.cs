# if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Reports the progress of the incremental achievement with the specified name.")]
    public class GameService_ReportAchievementProgress : FsmStateAction
    {
        [Tooltip("Name of the achievement to report progress. ")]
        public FsmString achievementName;

        [Tooltip("Achievement progress.")]
        public FsmFloat progress;

        [ActionSection("Result")]

        [Tooltip("True if the achievement progress was reported successfully, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Where to send the events.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent if the achievement progress was reported successfully.")]
        public FsmEvent successEvent;

        [Tooltip("Event sent if the achievement progress was failed to report.")]
        public FsmEvent failureEvent;

        public override void Reset()
        {
            progress = null;
            achievementName = null;
            isSuccess = false;
            eventTarget = null;
            successEvent = null;
            failureEvent = null;
        }

        public override void OnEnter()
        {
            if (GameServices.IsInitialized() && GameServices.GetAchievementByName(achievementName.Value) != null)
            {
                GameServices.ReportAchievementProgress(
                    achievementName.Value,
                    progress.Value,
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


