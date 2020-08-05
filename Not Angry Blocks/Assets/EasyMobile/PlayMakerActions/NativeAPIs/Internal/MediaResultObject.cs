using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EasyMobile.PlayerMaker.Actions.Internal
{
    public class MediaResultObject : Object
    {
        MediaResult media;

        public MediaResult Media
        {
            get
            {
                return this.media;
            }
            set
            {
                this.media = value;
            }
        }
    }
}
