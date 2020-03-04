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
    [Tooltip(" Reads the binary data of the specified saved game, which must be opened")]
    public class SavedGame_ReadSavedGameData : FsmStateAction
    {
        [Tooltip("Name of the saved game to read data.")]
        [RequiredField]
        public FsmString savedGameName;

        [ActionSection("Result")]

        [Tooltip("Event sent if no error occurs.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if an error occurs.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("True if no error occurs.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("The error description if any.")]
        [UIHint(UIHint.Variable)]
        public FsmString errorDescription;

        [Tooltip("The retrieved saved game data in form of a base64 string.")]
        [UIHint(UIHint.Variable)]
        public FsmString data;

        public override void Reset()
        {
            base.Reset();
            isSuccess = false;
            isSuccessEvent = null;
            eventTarget = null;
            isNotSuccessEvent = null;
            errorDescription = null;
            data = null;
        }

        public override void OnEnter()
        {
            GameServices.SavedGames.OpenWithAutomaticConflictResolution(savedGameName.Value, (openedSavedGame, error) =>
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        if (openedSavedGame != null)
                        {
                            // Saved game found. Now read its data.
                            GameServices.SavedGames.ReadSavedGameData(openedSavedGame, OnSavedGameRead);
                        }
                        else
                        {
                            // No saved game found with the specified name.
                            isSuccess.Value = false;
                            errorDescription.Value = "Not found saved game with name " + savedGameName;
                            Fsm.Event(eventTarget, isNotSuccessEvent);
                            Finish();
                        }
                    }
                    else
                    {
                        isSuccess.Value = false;
                        errorDescription.Value = error;
                        Fsm.Event(eventTarget, isNotSuccessEvent);
                        Finish();
                    }
                });
        }

        void OnSavedGameRead(SavedGame game, byte[] bytesData, string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                isSuccess.Value = false;
                errorDescription.Value = error;
                Fsm.Event(eventTarget, isNotSuccessEvent);
                Finish();
            }
            else
            {
                data.Value = Convert.ToBase64String(bytesData);
                isSuccess.Value = true;
                Fsm.Event(eventTarget, isSuccessEvent);
                Finish();
            }
        }
    }
}
#endif
