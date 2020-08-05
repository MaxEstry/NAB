#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.SocialPlatforms;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Saved Games")]
    [Tooltip("Opens the saved game with the specified name, or creates a new one if none exists."
        + "If the saved game has outstanding conflicts, they will be resolved automatically"
        + "using the conflict resolution strategy specified in the module settings." +
        "The returned saved game will be open, which means it can be used for" +
        "read or write operations.")]
    public class SavedGame_Open : FsmStateAction
    {
        [Tooltip("Name of saved game to open.")]
        [RequiredField]
        public FsmString savedGameName;

        [ActionSection("Result")]

        [Tooltip("Event sent if no error occur")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if an error occurs")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("True if no error occurs.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("The error description if any.")]
        [UIHint(UIHint.Variable)]
        public FsmString errorDescription;

        [ActionSection("Returned Saved Game Info")]

        [Tooltip("Name of the returned saved game")]
        [UIHint(UIHint.Variable)]
        public FsmString name;

        [Tooltip("The open status of the returned saved game")]
        [UIHint(UIHint.Variable)]
        public FsmBool isOpen;

        [Tooltip("A timestamp corresponding to the last modification to the underlying saved game. If the" +
            "saved game is newly created, this value will correspond to the time the first Open" +
            "occurred. Otherwise, this corresponds to time the last successful write or conflict resolution occurred.")]
        [UIHint(UIHint.Variable)]
        public FsmInt modificationDate_Year;
        [UIHint(UIHint.Variable)]
        public FsmInt modificationDate_Month;
        [UIHint(UIHint.Variable)]
        public FsmInt modificationDate_Day;
        [UIHint(UIHint.Variable)]
        public FsmInt modificationDate_Hour;
        [UIHint(UIHint.Variable)]
        public FsmInt modificationDate_Minute;
        [UIHint(UIHint.Variable)]
        public FsmInt modificationDate_Second;

        [Tooltip("[iOS only] The name of the device that committed the saved game data." +
            "If the saved game has just been opened with no data committed, this will be null." +
            "This value will be null on non-iOS platforms.")]
        [UIHint(UIHint.Variable)]
        public FsmString deviceName;

        [Tooltip("[GPGS only] If Google Play Game Services platform is in use, this returns" +
            "a human-readable description of the saved game, which may be null." +
            "This value will always be null if Google Play Games Services platform is not in use.")]
        [UIHint(UIHint.Variable)]
        public FsmString description;

        [Tooltip("[GPGS only] If Google Play Game Services platform is in use, this returns a URL corresponding to the " +
            "PNG-encoded image corresponding to this saved game, which may be null if the saved game does not have a cover image." +
            "This value will always be null if Google Play Games Services platform is not in use.")]
        [UIHint(UIHint.Variable)]
        public FsmString coverImageUrl;

        [Tooltip("[GPGS only] If Google Play Game Services platform is in use, this returns the total time (in seconds) played by " +
            "the player for this saved game. This value is developer-specified and may be tracked in" +
            "any way that is appropriate to the game. Note that this value is specific to this specific" +
            "saved game (unless the developer intentionally sets the same value on all saved games). " +
            "If the value was not set, or Google Play Games Services platform is not in use," +
            "this will be equal to 0.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat totalPlayedTime;

        public override void Reset()
        {
            base.Reset();
            savedGameName = null;
            isSuccess = false;
            errorDescription = null;
            eventTarget = null;
            isNotSuccessEvent = null;
            isSuccessEvent = null;

            name = null;
            isOpen = false;
            modificationDate_Year = null;
            modificationDate_Month = null;
            modificationDate_Day = null;
            modificationDate_Hour = null;
            modificationDate_Minute = null;
            modificationDate_Second = null;
            deviceName = null;
            description = null;
            coverImageUrl = null;
            totalPlayedTime = 0;
        }

        public override void OnEnter()
        {
            if (!string.IsNullOrEmpty(savedGameName.Value))
            {
                GameServices.SavedGames.OpenWithAutomaticConflictResolution(savedGameName.Value,
                    (SavedGame savedGame, string error) =>
                    {
                        if (string.IsNullOrEmpty(error))
                        {
                            name.Value = savedGame.Name;
                            isOpen.Value = savedGame.IsOpen;
                            modificationDate_Year.Value = savedGame.ModificationDate.Year;
                            modificationDate_Month.Value = savedGame.ModificationDate.Month;
                            modificationDate_Day.Value = savedGame.ModificationDate.Day;
                            modificationDate_Hour.Value = savedGame.ModificationDate.Hour;
                            modificationDate_Minute.Value = savedGame.ModificationDate.Minute;
                            modificationDate_Second.Value = savedGame.ModificationDate.Second;
                            deviceName.Value = savedGame.DeviceName;
                            description.Value = savedGame.Description;
                            coverImageUrl.Value = savedGame.CoverImageURL;
                            totalPlayedTime.Value = (float)savedGame.TotalTimePlayed.TotalSeconds;

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
                    });
            }
            else
            {
                errorDescription.Value = "Invalid saved game name.";
                isSuccess.Value = false;
                Fsm.Event(eventTarget, isNotSuccessEvent);
                Finish();
            }
        }
    }
}
#endif
