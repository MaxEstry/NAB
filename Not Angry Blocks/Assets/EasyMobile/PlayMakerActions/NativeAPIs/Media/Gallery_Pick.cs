#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Pick item(s) from gallery.")]
    public class Gallery_Pick : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("Event sent if get media successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if get media not successful.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("The number of medias received.")]
        [UIHint(UIHint.Variable)]
        public FsmInt mediaCount;

        [Tooltip("The Unity Object containing the all medias.")]
        [ArrayEditor(VariableType.Object)]
        public FsmArray medias;

        [Tooltip("Error message.")]
        public FsmString errorMsg;

        public override void Reset()
        {
            base.Reset();
            mediaCount = 0;
            medias = null;
        }

        public override void OnEnter()
        {
            Media.Gallery.Pick(OnMediasReceived);
        }

        void OnMediasReceived(string error, MediaResult[] mediasReceived)
        {
            if (string.IsNullOrEmpty(error) && mediasReceived != null)
            {
                mediaCount.Value = mediasReceived.Length;
                medias.Resize(mediaCount.Value);

                for (int i = 0; i < mediaCount.Value; i++)
                {
                    MediaResultObject contactObject = new MediaResultObject();

                    contactObject.Media = mediasReceived[i];

                    medias.Set(i, contactObject);
                }

                Fsm.Event(eventTarget, isSuccessEvent);
            }
            else
            {
                errorMsg.Value = error;
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }

        }
    }
}
#endif