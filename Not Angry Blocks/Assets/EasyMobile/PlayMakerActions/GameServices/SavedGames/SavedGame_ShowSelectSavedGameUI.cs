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
    [Tooltip("On Android/Google Play Games Services platform, this method shows the select saved game UI" +
        "with the indicated configuration. If the user selects a " +
        "saved game in that UI, it will be returned in the passed callback." +
        "This saved game will NOT be open and must be passed through the SavedGame_Open action" +
        "before it can be used for read or write operations.")]
    public class SavedGame_ShowSelectSavedGameUI : FsmStateAction
    {
        [Tooltip("The user-visible title of the displayed selection UI.")]
        [RequiredField]
        public FsmString uiTitle;

        [Tooltip("The maximum number of saved games the UI may display. This value must be greater than 0.")]
        [RequiredField]
        public FsmInt maxDisplayedSavedGames;

        [Tooltip("Show UI that will allow the user to create a new saved game.")]
        [RequiredField]
        public FsmBool showCreateSaveUI;

        [Tooltip("Show UI that will allow the user to delete a saved game.")]
        [RequiredField]
        public FsmBool showDeleteSaveUI;

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

        [ActionSection("Selected Saved Game")]

        [Tooltip("Name of the selected saved game if any.")]
        [UIHint(UIHint.Variable)]
        public FsmString savedGameName;

        public override void Reset()
        {
            uiTitle = null;
            maxDisplayedSavedGames = 0;
            showCreateSaveUI = null;
            showDeleteSaveUI = null;
            isNotSuccessEvent = null;
            isSuccessEvent = null;
            eventTarget = null;
            isSuccess = false;
            savedGameName = null;
        }

        public override void OnEnter()
        {
            GameServices.SavedGames.ShowSelectSavedGameUI(uiTitle.Value, (uint)maxDisplayedSavedGames.Value,
                showCreateSaveUI.Value, showDeleteSaveUI.Value, DefaultCallback);
        }

        void DefaultCallback(SavedGame savedGame, string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                savedGameName.Value = savedGame.Name;
                isSuccess.Value = true;
                Fsm.Event(eventTarget, isSuccessEvent);
            }
            else
            {
                isSuccess.Value = false;
                errorDescription.Value = error;
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }
            Finish();
        }
    }
}
#endif
