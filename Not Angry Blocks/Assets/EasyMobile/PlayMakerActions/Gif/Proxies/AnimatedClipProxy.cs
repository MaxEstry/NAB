#if PLAYMAKER
using UnityEngine;
using System.Collections;

namespace EasyMobile.PlayerMaker
{
    public class AnimatedClipProxy : ScriptableObject
    {
        public AnimatedClip clip { get; private set; }

        public static AnimatedClipProxy CreateClipProxy(AnimatedClip clip)
        {
            var proxy = CreateInstance<AnimatedClipProxy>();
            proxy.clip = clip;
            return proxy;
        }
    }
}
#endif