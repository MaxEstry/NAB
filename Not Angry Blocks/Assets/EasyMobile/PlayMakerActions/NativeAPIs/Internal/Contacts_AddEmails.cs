#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using EasyMobile.Internal;
using System.Collections.Generic;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Add Email.")]
    public class Contacts_AddEmail : FsmStateAction
    {
        [Tooltip("The Object that save all the emails.")]
        public FsmObject emailsObject;

        [Tooltip("The label")]
        public FsmString label;

        [Tooltip("The email")]
        public FsmString email;

        [ActionSection("Result")]

        [Tooltip("The Object that save all the emails after added.")]
        public FsmObject emailsObjectOut;

        public override void Reset()
        {
            base.Reset();
            label = null;
            email = null;
            emailsObject = null;
        }

        public override void OnEnter()
        {
            if (string.IsNullOrEmpty(label.Value))
            {
                NativeUI.Alert("Invalid email label", "Email's label can't be empty");
                Finish();
            }

            if (string.IsNullOrEmpty(email.Value))
            {
                NativeUI.Alert("Invalid email", "Email can't be empty");
                Finish();
            }
            AddedEmailsObject temp = (AddedEmailsObject)emailsObject.Value;

            List<StringStringKeyValuePair> addedEmails = temp.AddedEmails;
            if (addedEmails == null)
                addedEmails = new List<StringStringKeyValuePair>();
            addedEmails.Add(new StringStringKeyValuePair(label.Value, email.Value));

            temp.AddedEmails = addedEmails;
            emailsObjectOut.Value = temp;
            emailsObject.Value = temp;
            NativeUI.Alert("Success", "New email has been added.");
            Finish();

        }
    }
}
#endif