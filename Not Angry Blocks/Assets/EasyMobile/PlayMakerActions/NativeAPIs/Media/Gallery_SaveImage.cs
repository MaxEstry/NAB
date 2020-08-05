#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip(" Save an image into gallery.")]
    public class Gallery_SaveImage : FsmStateAction
    {
        [Tooltip("The image will be saved in this format.")]
        [ObjectType(typeof(ImageFormat))]
        public FsmEnum format;

        [Tooltip("Image will be saved with this name.")]
        public FsmString name;

        [Tooltip("The image you want to save.")]
        public FsmTexture image;

        [ActionSection("Result")]

        [Tooltip("Event sent if get media successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if get media not successful.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("The error message.")]
        public FsmString errorMsg;

        public override void Reset()
        {
            base.Reset();          
            name = null;
            image = null;
        }

        public override void OnEnter()
        {
            Media.Gallery.SaveImage((Texture2D)image.Value,name.Value,(ImageFormat)format.Value,OnError);
        }

        void OnError(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
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