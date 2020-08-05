#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;
using UnityEngine.UI;

namespace EasyMobile.PlayerMaker.Actions.Internal
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Get Texture from RawImage")] 
    public class DemoUtils_GetTexture : FsmStateAction
    {
        [Tooltip("The raw image containing the texture.")]
        public FsmOwnerDefault rawImage;

        [ActionSection("Result")]

        [Tooltip("Texture")]
        public FsmTexture texture;

        [Tooltip("Event sent if get texture successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if get texture not successful.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            rawImage = null;
        }

        public override void OnEnter()
        {

            RawImage raw = rawImage.GameObject.Value.GetComponent<RawImage>();

            if (raw.texture != null)
            {
                texture.Value = raw.texture;
                Fsm.Event(eventTarget, isSuccessEvent);
            }
            else
            {
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }
        }
    }
}

#endif