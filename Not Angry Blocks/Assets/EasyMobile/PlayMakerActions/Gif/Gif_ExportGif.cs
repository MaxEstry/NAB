#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Exports a GIF image from the provided clip.")]
    public class Gif_ExportGif : FsmStateAction
    {
        [Tooltip("The animated clip to generate the GIF image from.")]
        [RequiredField]
        [ObjectType(typeof(AnimatedClipProxy))]
        [UIHint(UIHint.Variable)]
        public FsmObject animatedClip;

        [Tooltip("Filename to save the output GIF.")]
        [RequiredField]
        public FsmString fileName;

        [Tooltip("Set to -1 to disable, 0 to loop indefinitely, >0 to loop a set number of times.")]
        public FsmInt loop;

        [Tooltip("Quality setting for the exported image. Inputs will be clamped between 1 and 100. Bigger values mean better quality but slightly longer processing time. 80 is generally a good value in terms of time-quality balance.")]
        [Range(1, 100)]
        public FsmInt quality;

        [Tooltip("Priority of the GIF encoding thread.")]
        public System.Threading.ThreadPriority threadPriority;

        [ActionSection("Result")]

        [Tooltip("The exporting progress running from 0 to 1.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat progress;

        [Tooltip("The filepath of the exported GIF.")]
        [UIHint(UIHint.Variable)]
        public FsmString gifFilePath;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent repeatedly while exporting.")]
        public FsmEvent exportingEvent;

        [Tooltip("Event sent once the export completed.")]
        public FsmEvent exportCompletedEvent;

        public override void Reset()
        {
            animatedClip = null;
            fileName = null;
            loop = 0;
            quality = 80;
            threadPriority = System.Threading.ThreadPriority.Normal;
            progress = null;
            gifFilePath = null;
            eventTarget = null;
            exportingEvent = null;
            exportCompletedEvent = null;
        }

        public override void OnEnter()
        {
            var clipProxy = (AnimatedClipProxy)animatedClip.Value;
            Gif.ExportGif(clipProxy.clip, fileName.Value, loop.Value, quality.Value, threadPriority, ExportProgressCallback, ExportCompletedCallback);
        }

        void ExportProgressCallback(AnimatedClip animatedClipCallback, float p)
        {
            progress.Value = p;
            Fsm.Event(eventTarget, exportingEvent);
        }

        void ExportCompletedCallback(AnimatedClip animatedClipCallback, string filepath)
        {
            gifFilePath.Value = filepath;
            Fsm.Event(eventTarget, exportCompletedEvent);
            Finish();
        }
    }
}

#endif
