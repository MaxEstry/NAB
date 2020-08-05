#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Media")]
    [Tooltip("Open device's camera to take a picture.")]
    public class Camera_TakePicture : FsmStateAction
    {
        [Tooltip("Type of the camera.")]
        [ObjectType(typeof(CameraType))]
        public FsmEnum cameraType;

        [ActionSection("Result")]

        [Tooltip("Event sent if take a picture successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if take a picture not successful.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("The Unity Object containing the MediaResult")]
        public FsmObject mediaObject;

        [Tooltip("Error message")]
        public FsmString errorMsg;

        public override void Reset()
        {
            base.Reset();
            mediaObject = null;
        }

        public override void OnEnter()
        {
            Media.Camera.TakePicture((CameraType)cameraType.Value,OnMediaReceived);
        }

        void OnMediaReceived(string error,MediaResult mediaReceived)
        {
            if (string.IsNullOrEmpty(error) && mediaReceived != null)
            {
                MediaResultObject contactObjectTemp = new MediaResultObject();

                contactObjectTemp.Media = mediaReceived;

                mediaObject.Value = contactObjectTemp;
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