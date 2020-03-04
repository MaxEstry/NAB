#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Sharing")]
    [Tooltip("Shares the local image at the specified path using the native sharing utility.")]
    public class MobileNativeShare_ShareImage : FsmStateAction
    {
        [Tooltip("The path of the sharing image.")]
        [RequiredField]
        public FsmString filepath;

        [Tooltip("[Optional] The sharing message")]
        public FsmString message;

        [Tooltip("[Optional] The sharing subject.")]
        public FsmString subject;

        public override void Reset()
        {
            filepath = null;
            message = "";
            subject = "";
        }

        public override void OnEnter()
        {
            Sharing.ShareImage(filepath.Value, message.Value, subject.Value);
            Finish();
        }
    }
}

#endif