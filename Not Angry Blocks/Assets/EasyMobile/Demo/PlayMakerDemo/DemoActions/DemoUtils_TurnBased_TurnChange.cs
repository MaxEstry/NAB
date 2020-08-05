#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Call after turn changes")]
    public class DemoUtils_TurnBased_TurnChange : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(DemoUtils_TurnBasedManager))]
        [Tooltip("Object contain current match data")]
        public FsmGameObject turnBaseManager;

        [Tooltip("Set canTakeTurn with this value")]
        public FsmBool canTakeTurn;

        public override void Reset()
        {
            base.Reset();
            turnBaseManager = null;
            canTakeTurn = false;
        }

        public override void OnEnter()
        {
            DemoUtils_TurnBasedManager manager = turnBaseManager.Value.GetComponent<DemoUtils_TurnBasedManager>();

            string id = manager.CurrentMatch.MatchId;

            if (manager != null)
            {
                manager.canTakeTurn = canTakeTurn.Value;
                GameServices.TurnBased.GetAllMatches((TurnBasedMatch[] matches) =>
                {
                    foreach (TurnBasedMatch m in matches)
                    {
                        if (m.MatchId == id)
                        {
                            manager.CurrentMatch = m;
                        }
                    }
                });
                Finish();
            }
        }
    }
}

#endif