#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Starts recording on the specified recorder.")]
    public class Gif_StartRecording : FsmStateAction
    {
        [Tooltip("The recorder.")]
        [RequiredField]
        [ObjectType(typeof(Recorder))]
        public FsmObject recorder;

        public override void Reset()
        {
            recorder = null;
        }

        public override void OnEnter()
        {
            Gif.StartRecording((Recorder)recorder.Value);
            Finish();
        }
    }
}

#endif
