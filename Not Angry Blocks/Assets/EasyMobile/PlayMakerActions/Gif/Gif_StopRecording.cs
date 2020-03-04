#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Stops recording on the specified recorder.")]
    public class Gif_StopRecording : FsmStateAction
    {
        [Tooltip("The recorder")]
        [RequiredField]
        [ObjectType(typeof(Recorder))]
        public FsmObject recorder;

        [ActionSection("Result")]

        [Tooltip("The recorded clip. Assign an AnimatedClipProxy variable for this parameter.")]
        [ObjectType(typeof(AnimatedClipProxy))]
        [UIHint(UIHint.Variable)]
        public FsmObject recordedClip;

        public override void Reset()
        {
            recorder = null;
            recordedClip = null;
        }

        public override void OnEnter()
        {
            var newClip = Gif.StopRecording((Recorder)recorder.Value);
            var newClipProxy = AnimatedClipProxy.CreateClipProxy(newClip);
            recordedClip.Value = newClipProxy;

            Finish();
        }
    }
}

#endif
