#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Reveals the hidden achievement with the specified name.")]
    public class GameService_RevealAchievement : FsmStateAction
    {
        [Tooltip("Name of the achievement to reveal.")]
        public FsmString achievementName;

        [ActionSection("Result")]

        [Tooltip("True if the achievement was revealed successfully, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Where to send the events.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent if the achievement was revealed successfully.")]
        public FsmEvent successEvent;

        [Tooltip("Event sent if the achievement was failed to reveal.")]
        public FsmEvent failureEvent;

        public override void Reset()
        {
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
                GameServices.RevealAchievement(
                    achievementName.Value,
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
