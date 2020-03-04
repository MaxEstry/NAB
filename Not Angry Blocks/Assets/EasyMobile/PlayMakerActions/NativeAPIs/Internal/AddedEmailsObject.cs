using System.Collections;
using System.Collections.Generic;
using EasyMobile.Internal;
using UnityEngine;


namespace EasyMobile.PlayerMaker.Actions.Internal
{
    public class AddedEmailsObject : Object
    {
        private List<StringStringKeyValuePair> addedEmails;

        public List<StringStringKeyValuePair> AddedEmails
        {
            get
            {
                return this.addedEmails;
            }
            set
            {
                this.addedEmails = value;
            }
        }
    }
}
