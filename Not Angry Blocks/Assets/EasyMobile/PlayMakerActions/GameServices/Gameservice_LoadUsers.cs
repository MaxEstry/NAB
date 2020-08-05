#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.SocialPlatforms;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Loads the user profiles associated with the given array of user IDs." +
        "The 'Users Loaded Event' is sent when the user profiles are loaded successfully. " +
        "The action finishes either when the profiles are loaded, or the loading " +
        "fails due to the service not being initialized.")]
    public class GameService_LoadUsers : FsmStateAction
    {
        [Tooltip("The IDs of the users to load profiles.")]
        [ArrayEditor(VariableType.String)]
        public FsmArray userIds;

        [ActionSection("Result")]

        [Tooltip("The number of users loaded.")]
        [UIHint(UIHint.Variable)]
        public FsmInt userCount;

        [Tooltip("The IDs of the loaded users. Each user has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray idArray;

        [Tooltip("The user names of the loaded users. Each user has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray userNameArray;

        [Tooltip("The images of the loaded users. Each user has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(typeof(Texture2D))]
        public FsmArray imageArray;

        [Tooltip("The states of the loaded users. Each user has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(typeof(UserState))]
        public FsmArray userStateArray;

        [Tooltip("True if the user is a friend of the local user. Each user has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Bool)]
        public FsmArray isFriendArray;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when user profiles are loaded.")]
        public FsmEvent usersLoadedEvent;

        public override void Reset()
        {
            userCount = null;
            idArray = null;
            userNameArray = null;
            imageArray = null;
            userStateArray = null;
            isFriendArray = null;
            eventTarget = null;
            usersLoadedEvent = null;
        }

        public override void OnEnter()
        {
            if (GameServices.IsInitialized())
                GameServices.LoadUsers(userIds.stringValues, UsersLoadedCallback);
            else
                Finish();
        }

        void UsersLoadedCallback(IUserProfile[] profiles)
        {
            userCount.Value = profiles.Length;

            for (int i = 0; i < userCount.Value; i++)
            {
                idArray.Values[i] = profiles[i].id;
                userNameArray.Values[i] = profiles[i].userName;
                imageArray.Values[i] = (object)profiles[i].image;
                userStateArray.Values[i] = (object)profiles[i].state;
                isFriendArray.Values[i] = profiles[i].isFriend;
            }
                
            Fsm.Event(eventTarget, usersLoadedEvent);
            Finish();
        }
    }
}
#endif