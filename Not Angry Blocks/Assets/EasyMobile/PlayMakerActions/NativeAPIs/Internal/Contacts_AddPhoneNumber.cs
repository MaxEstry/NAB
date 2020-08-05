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
    [Tooltip("Add phone number.")]
    public class Contacts_AddPhoneNumber : FsmStateAction
    {
        [Tooltip("The Object that save all the phone numbers.")]
        public FsmObject phoneNumbersObject;

        [Tooltip("The label")]
        public FsmString label;

        [Tooltip("The phoneNumber")]
        public FsmString phoneNumber;

        [ActionSection("Result")]

        [Tooltip("The Object that save all the phone numbers after added.")]
        public FsmObject phoneNumbersObjectOut;

        public override void Reset()
        {
            base.Reset();
            label = null;
            phoneNumber = null;
            phoneNumbersObject = null;
        }

        public override void OnEnter()
        {
            if (string.IsNullOrEmpty(label.Value))
            {
                NativeUI.Alert("Invalid phone number's label", "Phone number's label can't be empty");
                Finish();
                return;
            }

            if (string.IsNullOrEmpty(phoneNumber.Value))
            {
                NativeUI.Alert("Invalid phone number", "Phone number can't be empty");
                Finish();
                return;
            }
            AddedPhoneNumbersObject temp = (AddedPhoneNumbersObject)phoneNumbersObject.Value;

            List<StringStringKeyValuePair> addedPhoneNumbers = temp.AddedPhoneNumbers;
            if (addedPhoneNumbers == null)
                addedPhoneNumbers = new List<StringStringKeyValuePair>();
            addedPhoneNumbers.Add(new StringStringKeyValuePair(label.Value, phoneNumber.Value));

            temp.AddedPhoneNumbers = addedPhoneNumbers;
            phoneNumbersObjectOut.Value = temp;
            phoneNumbersObject.Value = temp;
            NativeUI.Alert("Success", "New phone number has been added.");
            Finish();

        }
    }
}
#endif