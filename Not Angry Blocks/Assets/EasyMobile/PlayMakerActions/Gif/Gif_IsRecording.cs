#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Determines whether the specified recorder is recording.")]
    public class Gif_IsRecording : FsmStateAction
    {
        [Tooltip("The recorder.")]
        [RequiredField]
        [ObjectType(typeof(Recorder))]
        public FsmObject recorder;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if the recorder is recording, False otherwise.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isRecording;

        [Tooltip("Event sent if the recorder is recording.")]
        public FsmEvent isRecordingEvent;

        [Tooltip("Event sent if the recorder is not recording.")]
        public FsmEvent isNotRecordingEvent;

        public override void Reset()
        {
            everyFrame = false;
            recorder = null;
            isRecording = null;
        }

        public override void OnEnter()
        {
            DoMyAction();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoMyAction();
        }

        void DoMyAction()
        {
            isRecording.Value = Gif.IsRecording((Recorder)recorder.Value);

            if (isRecording.Value)
                Fsm.Event(isRecordingEvent);
            else
                Fsm.Event(isNotRecordingEvent);
        }
    }
}

#endif
