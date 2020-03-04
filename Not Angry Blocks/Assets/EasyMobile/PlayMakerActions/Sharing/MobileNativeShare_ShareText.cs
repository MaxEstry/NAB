#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Sharing")]
    [Tooltip("Shares a text using the native sharing utility." +
        "Note that Facebook doesn't allow pre-filled sharing message," +
        "so the text will be discarded when sharing to that particular network.")]
    public class MobileNativeShare_ShareText : FsmStateAction
    {
        [Tooltip("The sharing text.")]
        [RequiredField]
        public FsmString text;

        [Tooltip("[Optional] The sharing subject.")]
        public FsmString subject;

        public override void Reset()
        {
            text = null;
            subject = "";
        }

        public override void OnEnter()
        {
            Sharing.ShareText(text.Value, subject.Value);
            Finish();
        }
    }
}

#endif