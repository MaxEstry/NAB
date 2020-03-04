#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Sharing")]
    [Tooltip("Shares a URL using the native sharing utility.")]
    public class MobileNativeShare_ShareURL : FsmStateAction
    {
        [Tooltip("The sharing Url.")]
        [RequiredField]
        public FsmString url;

        [Tooltip("[Optional] The sharing subject.")]
        public FsmString subject;

        public override void Reset()
        {
            url = null;
            subject = "";
        }

        public override void OnEnter()
        {
            Sharing.ShareURL(url.Value, subject.Value);
            Finish();
        }
    }
}

#endif