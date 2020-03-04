#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using EasyMobile.Internal;
using System.Collections.Generic;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Init Contact Scene")]
    public class Contacts_InitScene : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The Object that save all the phone numbers.")]
        public FsmObject phoneNumbersObjectOut;

        [Tooltip("The Object that save all the emails.")]
        public FsmObject emailsObjectOut;

        public override void Reset()
        {
            phoneNumbersObjectOut = null;
            emailsObjectOut = null;
        }

        public override void OnEnter()
        {
            phoneNumbersObjectOut.Value = new AddedPhoneNumbersObject();
            emailsObjectOut.Value = new AddedEmailsObject();
            Finish();
        }
    }
}
#endif