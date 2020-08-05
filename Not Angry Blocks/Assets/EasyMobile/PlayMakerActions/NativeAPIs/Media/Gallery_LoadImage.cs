#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Load image from MediaResult.")]
    public class Gallery_LoadImage : FsmStateAction
    {
        [Tooltip("Target result, note that this method only work if the MediaResult.Type equals MediaType.Image")]
        public FsmObject mediaObject;

        [Tooltip("Maximum size of the image. Load fullsize if non-positive.")]
        public FsmInt maxSize;

        [ActionSection("Result")]

        [Tooltip("Event sent if load image successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if load image not successful.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("The received image.")]
        public FsmTexture image;

        [Tooltip("The error message.")]
        public FsmString errorMsg;

        public override void Reset()
        {
            base.Reset();
            mediaObject = null;
            maxSize = -1;
            image = null;
        }

        public override void OnEnter()
        {
            MediaResultObject tempObj = (MediaResultObject)mediaObject.Value;

            Media.Gallery.LoadImage(tempObj.Media, OnImageReceived,maxSize.Value);
        }

        void OnImageReceived(string error, Texture2D imageReceived)
        {
            if (string.IsNullOrEmpty(error))
            {
                image.Value = imageReceived;
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