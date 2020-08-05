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
    [Tooltip("Deletes the specified saved game." +
        "This will delete the data of the saved game locally and on the cloud.")]
    public class SavedGame_DeleteSavedGame : FsmStateAction
    {
        [Tooltip("Name of the saved game to delete")]
        [RequiredField]
        public FsmString savedGameName;

        public override void Reset()
        {
            savedGameName = null;
        }

        public override void OnEnter()
        {
            GameServices.SavedGames.OpenWithAutomaticConflictResolution(savedGameName.Value, (openedSavedGame, error) =>
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        if (openedSavedGame != null)
                        {
                            GameServices.SavedGames.DeleteSavedGame(openedSavedGame);
                        }
                    }

                    Finish();
                });
        }
    }
}
#endif