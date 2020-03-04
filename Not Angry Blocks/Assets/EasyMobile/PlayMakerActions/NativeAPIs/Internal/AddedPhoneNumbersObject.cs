using System.Collections;
using System.Collections.Generic;
using EasyMobile.Internal;
using UnityEngine;


namespace EasyMobile.PlayerMaker.Actions.Internal
{
    public class AddedPhoneNumbersObject : Object
    {
        private List<StringStringKeyValuePair> addedPhoneNumbers;


        public List<StringStringKeyValuePair> AddedPhoneNumbers
        {
            get
            {
                return this.addedPhoneNumbers;
            }
            set
            {
                this.addedPhoneNumbers = value;
            }
        }
    }
}
