#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.SocialPlatforms;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Game Services")]
    [Tooltip("Loads the profiles of all friends of the authenticated (local) user." +
        "The 'Friends Loaded Event' is sent when the friend profiles are loaded successfully. " +
        "The action finishes either when the friend profiles are loaded, or the loading " +
        "fails due to the service not being initialized.")]
    public class GameService_LoadFriends : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The number of friends loaded.")]
        [UIHint(UIHint.Variable)]
        public FsmInt friendCount;

        [Tooltip("The IDs of the loaded friends. Each friend has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray idArray;

        [Tooltip("The user names of the loaded friends. Each friend has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray userNameArray;

        [Tooltip("The images of the loaded friends. Each friend has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(typeof(Texture2D))]
        public FsmArray imageArray;

        [Tooltip("The states of the loaded friends. Each friend has same indices in all the resulted data arrays.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(typeof(UserState))]
        public FsmArray userStateArray;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when friend profiles are loaded.")]
        public FsmEvent friendsLoadedEvent;

        public override void Reset()
        {
            friendCount = null;
            idArray = null;
            userNameArray = null;
            imageArray = null;
            userStateArray = null;
            eventTarget = null;
            friendsLoadedEvent = null;
        }

        public override void OnEnter()
        {
            if (GameServices.IsInitialized())
                GameServices.LoadFriends(FriendsLoadedCallback);
            else
            {
                Finish();
            }
        }

        void FriendsLoadedCallback(IUserProfile[] profiles)
        {
            friendCount.Value = profiles.Length;

            for (int i = 0; i < friendCount.Value; i++)
            {
                idArray.Values[i] = profiles[i].id;
                userNameArray.Values[i] = profiles[i].userName;
                imageArray.Values[i] = (object)profiles[i].image;
                userStateArray.Values[i] = (object)profiles[i].state;
            }
                
            Fsm.Event(eventTarget, friendsLoadedEvent);
            Finish();
        }
    }
}
#endif