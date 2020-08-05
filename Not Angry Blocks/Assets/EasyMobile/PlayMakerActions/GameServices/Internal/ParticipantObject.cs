using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EasyMobile.PlayerMaker.Actions.Internal
{
    public class ParticipantObject : Object
    {
        private Participant participant;

        public Texture2D image;

        public Participant ParticipantData
        {
            get
            {
                return this.participant;
            }
            set
            {
                this.participant = value;
            }
        }

        public Texture2D Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
            }
        }
    }
}
