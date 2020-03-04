#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Disposes the specified animated clip. Required after you have done with using the clip.")]
    public class Gif_DisposeClip : FsmStateAction
    {
        [Tooltip("The animated clip to dispose. Use an AnimatedClipProxy variable for this parameter.")]
        [RequiredField]
        [ObjectType(typeof(AnimatedClipProxy))]
        [UIHint(UIHint.Variable)]
        public FsmObject clip;

        public override void Reset()
        {
            clip = null;
        }

        public override void OnEnter()
        {
            if (clip.Value != null)
            {
                var clipProxy = (AnimatedClipProxy)clip.Value;

                if (clipProxy.clip != null)
                {
                    clipProxy.clip.Dispose();
                    UnityEngine.Object.Destroy(clipProxy);
                    clip.Value = null;
                }
            }

            Finish();
        }
    }
}

#endif
