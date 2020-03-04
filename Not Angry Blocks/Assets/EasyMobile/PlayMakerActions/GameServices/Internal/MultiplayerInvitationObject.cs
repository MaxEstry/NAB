using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EasyMobile.PlayerMaker.Actions.Internal
{
    public class MultiplayerInvitationObject : Object
    {
        Invitation invitation;
        
        public Invitation Invitation
        {
            get
            {
                return this.invitation;
            }
            set
            {
                this.invitation = value;
            }
        }
    }
}
