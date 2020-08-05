#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Media")]
    [Tooltip("Determines whether the camera of the specified type is available on the current device.")]
    public class Camera_IsCameraAvailable : FsmStateAction
    {
        [Tooltip("Type of the camera.")]
        [ObjectType(typeof(CameraType))]
        public FsmEnum cameraType;

        [Tooltip("Repeats every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if camera is available.")]
        [UIHint(UIHint.Variable)]
        public FsmBool IsAvailable;

        public override void Reset()
        {
            IsAvailable = null;
            everyFrame = false;
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
            IsAvailable.Value = Media.Camera.IsCameraAvailable((CameraType)cameraType.Value);
        }
    }
}
#endif