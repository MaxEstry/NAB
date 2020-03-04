#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Reads the data of the specified animated clip into resulted variables.")]
    public class Gif_GetAnimatedClipContent : FsmStateAction
    {
        [Tooltip("The target animated clip. Use an AnimatedClipProxy variable for this.")]
        [RequiredField]
        [ObjectType(typeof(AnimatedClipProxy))]
        [UIHint(UIHint.Variable)]
        public FsmObject clip;

        [ActionSection("Result")]

        [Tooltip("The clip width in pixels.")]
        [UIHint(UIHint.Variable)]
        public FsmInt width;

        [Tooltip("The clip height in pixels.")]
        [UIHint(UIHint.Variable)]
        public FsmInt height;

        [Tooltip("The frame-per-second of the clip.")]
        [UIHint(UIHint.Variable)]
        public FsmInt fps;

        [Tooltip("The length of the clip in seconds.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat length;

        [Tooltip("The frames of the clip.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(typeof(RenderTexture))]
        public FsmArray frames;

        public override void Reset()
        {
            clip = null;
            width = null;
            height = null;
            fps = null;
            length = null;
            frames = null;  
        }

        public override void OnEnter()
        {            
            if (clip.Value != null)
            {
                var clipProxy = (AnimatedClipProxy)clip.Value;

                if (clipProxy.clip != null)
                {
                    AnimatedClip animClip = clipProxy.clip;
                    width.Value = animClip.Width;
                    height.Value = animClip.Height;
                    fps.Value = animClip.FramePerSecond;
                    length.Value = animClip.Length;
                    frames.Values = animClip.Frames;
                }
            }
                
            Finish();
        }
    }
}

#endif
