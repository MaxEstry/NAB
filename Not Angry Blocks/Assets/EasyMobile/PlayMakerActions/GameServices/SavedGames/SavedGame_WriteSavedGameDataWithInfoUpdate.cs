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
    [Tooltip("Writes the binary data to the specified saved game, optionally updates the saved game's info. When this method returns successfully,"
        + "the data is durably persisted to disk and will eventually be uploaded to the cloud (in"
        + "practice, this will happen very quickly unless the device does not have a network connection). " +
        "If an update to the saved game has occurred after it was retrieved " +
        "from the cloud, this commit will produce a conflict (this commonly occurs if two different " +
        "devices are writing to the cloud at the same time). All conflicts must be handled the next time this saved game is opened.")]
    public class SavedGame_WriteSavedGameDataWithInfoUpdate : FsmStateAction
    {
        [Tooltip("Name of saved game to write to.")]
        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmString savedGameName;

        [Tooltip("Data to write into the saved game in form of a base64 string.")]
        [RequiredField]
        public FsmString data;

        [Tooltip("[Optional] New description for this saved game.")]
        public FsmString newDescription;

        [Tooltip("[Optional] New PNG cover image for this saved game.")]
        public FsmTexture newPngCoverImage;

        [Tooltip("[Optional] New played time (in total seconds) for this saved game.")]
        public FsmFloat newPlayedTime;

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

        public override void Reset()
        {
            savedGameName = null;
            data = null;
            newDescription = null;
            newPngCoverImage = null;
            newPlayedTime = -1;
            isSuccess = null;
            errorDescription = null;
            isNotSuccessEvent = null;
            isSuccessEvent = null;
            eventTarget = null;
        }

        public override void OnEnter()
        {
            GameServices.SavedGames.OpenWithAutomaticConflictResolution(savedGameName.Value, (openedSavedGame, error) =>
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        if (openedSavedGame != null)
                        {
                            // Saved game found.
                            // Convert base64 string to binary data and write it to the found saved game.
                            byte[] binaryData = Convert.FromBase64String(data.Value);

                            // Update saved game's info.
                            SavedGameInfoUpdate.Builder builder = new SavedGameInfoUpdate.Builder();

                            if (!string.IsNullOrEmpty(newDescription.Value))
                                builder.WithUpdatedDescription(newDescription.Value);

                            if (newPngCoverImage.Value != null)
                            {
                                Texture2D tex = newPngCoverImage.Value as Texture2D;
                                builder.WithUpdatedPngCoverImage(tex.EncodeToPNG());
                            }

                            if (newPlayedTime.Value > 0)
                                builder.WithUpdatedPlayedTime(TimeSpan.FromSeconds(newPlayedTime.Value));

                            // Write data.
                            GameServices.SavedGames.WriteSavedGameData(openedSavedGame, binaryData, builder.Build(), OnSavedGameUpdated);
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

        void OnSavedGameUpdated(SavedGame game, string error)
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
                isSuccess.Value = true;
                Fsm.Event(eventTarget, isSuccessEvent);
                Finish();
            }
        }
    }
}
#endif
