#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using EasyMobile.PlayerMaker.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Called when receive a match")]
    public class DemoUtils_TurnBased_OnMatchReceived : FsmStateAction
    {
        [Tooltip("The Unity Object containing the TurnBasedMatch data")]
        public FsmObject matchObject;

        [Tooltip("If this is true, then the game should immediately proceed to the" +
           " game screen to play this match, without prompting the user.")]
        public FsmBool shouldAutoLaunch;

        [Tooltip("This is true only if the local player has removed the match in the" +
            " matches UI while being the turn holder. When this happens you should call the.")]
        public FsmBool playerWantsToQuit;

        [RequiredField]
        [CheckForComponent(typeof(DemoUtils_TurnBasedManager))]
        [Tooltip("Object contain current match data")]
        public FsmGameObject turnBaseManager;

        public override void Reset()
        {
            base.Reset();
            turnBaseManager = null;
            matchObject = null;
        }

        public override void OnEnter()
        {
            DemoUtils_TurnBasedManager manager = turnBaseManager.Value.GetComponent<DemoUtils_TurnBasedManager>();

            TurnBasedMatchObject temp = (TurnBasedMatchObject)matchObject.Value;

            TurnBasedMatch match = temp.Match;

            manager.OnMatchReceived(match, shouldAutoLaunch.Value, playerWantsToQuit.Value);

            Finish();
        }


    }
}

#endif