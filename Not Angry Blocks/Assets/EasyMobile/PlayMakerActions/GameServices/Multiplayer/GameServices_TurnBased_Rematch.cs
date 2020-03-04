#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using System;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Requests a rematch.")]
    public class GameServices_TurnBased_Rematch : FsmStateAction
    {
        [Tooltip("The Unity Object containing the current TurnBasedMatch data")]
        public FsmObject matchObject;

        [ActionSection("Result")]

        [Tooltip("The Unity Object containing the new TurnBasedMatch data")]
        public FsmObject matchObjectOut;

        [Tooltip("Match Data in base64 string format.")]
        public FsmString data;

        [Tooltip("True if success.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("Event sent if Rematch successful. ")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if Rematch not successful. ")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            matchObject = null;
            matchObjectOut = null;
            data = null;
        }

        public override void OnEnter()
        {
            TurnBasedMatchObject temp = (TurnBasedMatchObject)matchObject.Value;

            TurnBasedMatch match = temp.Match;

            GameServices.TurnBased.Rematch(match, RematchCallback);          
        }

        void RematchCallback(bool isRematchSuccess,TurnBasedMatch match)
        {           
            if (isRematchSuccess)
            {  
                TurnBasedMatchObject temp = new TurnBasedMatchObject();
                temp.Match = match;
                matchObjectOut.Value = temp;

                if (match.Data != null)
                {
                    data.Value = Convert.ToBase64String(match.Data);
                }
                else
                {
                    data.Value = null;
                }
                
                
                isSuccess.Value = true;
                Fsm.Event(eventTarget, isSuccessEvent);
            }
            else
            {
                isSuccess.Value = false;               
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }        
        }
    }
}

#endif