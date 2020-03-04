using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EasyMobile.PlayerMaker.Actions.Internal
{
    public class TurnBasedMatchObject : Object
    {
        TurnBasedMatch match;
        
        public TurnBasedMatch Match
        {
            get
            {
                return this.match;
            }
            set
            {
                this.match = value;
            }
        }
    }
}
