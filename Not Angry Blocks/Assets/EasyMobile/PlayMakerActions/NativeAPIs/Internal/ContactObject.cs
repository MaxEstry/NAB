using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EasyMobile.PlayerMaker.Actions.Internal
{
    public class ContactObject : Object
    {
        Contact contact;

        public Contact Contact
        {
            get
            {
                return this.contact;
            }
            set
            {
                this.contact = value;
            }
        }
    }
}
