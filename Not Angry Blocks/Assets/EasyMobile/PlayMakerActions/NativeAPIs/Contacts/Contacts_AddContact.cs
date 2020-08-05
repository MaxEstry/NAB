#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using EasyMobile.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Add new contact into the device.")]
    public class Contacts_AddContact: FsmStateAction
    {
       // private InputField firstnNameInput = null,
        //              middleNameInput = null,
            //lastNameInput = null,
           //           companyInput = null,
           //          birthdayInput = null,


        [Tooltip("First name")]
        public FsmString firstName;

        [Tooltip("Middle name")]
        public FsmString middleName;

        [Tooltip("Last Name")]
        public FsmString lastName;

        [Tooltip("Company")]
        public FsmString company;

        [Tooltip("Birthday")]
        public FsmString birthdayInput;

        [Tooltip("The Object that save all the phone numbers.")]
        public FsmObject phoneNumbersObject;

        [Tooltip("The Object that save all the emails.")]
        public FsmObject emailsObject;

        [Tooltip("Avatar")]
        public FsmTexture avatarImageInput;

        [ActionSection("Result")]

        [Tooltip("Event sent if add contact successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if add contact not successfully.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Error message. Null if success")]
        public FsmString errorMsg;

        public override void Reset()
        {
            base.Reset();
            firstName = null;
            middleName = null;
            lastName = null;
            company = null;
            birthdayInput = null;
            phoneNumbersObject = null;
            emailsObject = null;
            avatarImageInput = null;    
        }

        public override void OnEnter()
        {
            DateTime? birthday = null;
            if (!string.IsNullOrEmpty(birthdayInput.Value))
            {
                DateTime parseBirthday;
                if (DateTime.TryParseExact(birthdayInput.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parseBirthday))
                {
                    birthday = parseBirthday;
                }
                else
                {
                    errorMsg.Value = "Can't convert birthday to yyyy-MM-dd format.";
                    Fsm.Event(eventTarget, isNotSuccessEvent);
                }
            }

            AddedPhoneNumbersObject addedPhoneNumbersTemp = (AddedPhoneNumbersObject)phoneNumbersObject.Value;

            AddedEmailsObject addedEmailsTemp = (AddedEmailsObject)emailsObject.Value;

            List<StringStringKeyValuePair> addedEmails = addedEmailsTemp.AddedEmails != null ? addedEmailsTemp.AddedEmails : new List<StringStringKeyValuePair>();

            List<StringStringKeyValuePair> addedPhoneNumbers = addedPhoneNumbersTemp.AddedPhoneNumbers != null ? addedPhoneNumbersTemp.AddedPhoneNumbers : new List<StringStringKeyValuePair>();

            Texture2D avatarImage = (Texture2D)avatarImageInput.Value;

            //var avatar = avatarImage != null ? (Texture2D)avatarImage : null;

            Contact contact = new Contact()
            {
                FirstName = firstName.Value,
                MiddleName = middleName.Value,
                LastName = lastName.Value,
                Company = company.Value,
                Birthday = birthday,
                Emails = addedEmails.Select(email => new KeyValuePair<string, string>(email.Key, email.Value)).ToArray(),
                PhoneNumbers = addedPhoneNumbers.Select(phoneNumber => new KeyValuePair<string, string>(phoneNumber.Key, phoneNumber.Value)).ToArray(),
                Photo = avatarImage
            };

            string error = DeviceContacts.AddContact(contact);

            if (string.IsNullOrEmpty(error))
            {
                Fsm.Event(eventTarget, isSuccessEvent);
            }
            else
            {
                errorMsg.Value = error;
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }
        }
       
    }
}
#endif