#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.SocialPlatforms;
using System;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Saved Games")]
    [Tooltip("Retrieves all known saved games. All returned saved games are" +
        "not open, and must be opened before they can be used for read or write operations."
        + "On Android/Google Play Games Services platform, this method uses the data source specified in the module settings." +
        "The returned saved games will NOT be open and must be passed through the SavedGame_Open action" +
        "before it can be used for read or write operations.")]
    public class SavedGame_FetchAllSavedGames : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("Event sent if no error occurs.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if an error occurs.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("True if no error occurs.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("The error description if any.")]
        [UIHint(UIHint.Variable)]
        public FsmString errorDescription;

        [ActionSection("Fetched Saved Games")]

        [Tooltip("Number of fetched saved games.")]
        [UIHint(UIHint.Variable)]
        public FsmInt savedGamesCount;

        [Tooltip("Names of the fetched saved games.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray savedGameNames;

        public override void Reset()
        {
            base.Reset();
            isSuccess = false;
            isSuccessEvent = null;
            isNotSuccessEvent = null;
            eventTarget = null;
            errorDescription = null;
            savedGamesCount = 0;
            savedGameNames = null;
        }

        public override void OnEnter()
        {
            GameServices.SavedGames.FetchAllSavedGames(FetchAllSavedGamesAction);
        }

        void FetchAllSavedGamesAction(SavedGame[] allSavedGames, string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                savedGamesCount.Value = allSavedGames.Length;
                savedGameNames.stringValues = new string[allSavedGames.Length];

                for (int i = 0; i < allSavedGames.Length; i++)
                {
                    savedGameNames.stringValues[i] = allSavedGames[i].Name;
                }
                    
                isSuccess.Value = true;
                Fsm.Event(eventTarget, isSuccessEvent);
                Finish();
            }
            else
            {
                isSuccess.Value = false;
                errorDescription.Value = error;
                Fsm.Event(isNotSuccessEvent);
                Finish();
            }
        }
    }
}
#endif
