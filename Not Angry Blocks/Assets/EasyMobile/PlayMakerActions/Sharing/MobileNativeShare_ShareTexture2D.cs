#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Sharing")]
    [Tooltip("Generates a PNG image from the given Texture2D object, saves it to persistentDataPath with " +
        "the given filename and then shares the image via the native sharing utility.")]
    public class MobileNativeShare_ShareTexture2D : FsmStateAction
    {
        [Tooltip("The sharing Texture 2D.")]
        [RequiredField]
        public FsmTexture texture;

        [Tooltip("The filename to save the generated image, no need the .png extension.")]
        [RequiredField]
        public FsmString fileName;

        [Tooltip("[Optional] The sharing message")]
        public FsmString message;

        [Tooltip("[Optional]  The sharing subject.")]
        public FsmString subject;

        [ActionSection("Result")]

        [Tooltip("The filepath of the saved image.")]
        [UIHint(UIHint.Variable)]
        public FsmString filepath;

        public override void Reset()
        {
            texture = null;
            fileName = null;
            message = "";
            subject = "";
            filepath = null;
        }

        public override void OnEnter()
        {
            filepath.Value = Sharing.ShareTexture2D((Texture2D)texture.Value, fileName.Value, message.Value, subject.Value);
            Finish();
        }
    }
}

#endif