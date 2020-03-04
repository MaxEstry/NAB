#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Handle Received Message")]
    public class DemoUtils_RealTime_CloseInvitationPanel : FsmStateAction
    {
        [Tooltip("The Unity Object containing the all unhandled invitations")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Object)]
        public FsmArray unhandledInvitations;

        [Tooltip("The Invitation Panel")]
        public FsmGameObject invitationPanel;

        [ActionSection("Result")]

        [Tooltip("The Unity Object containing the invitation data")]
        public FsmObject invitationObject;

        [Tooltip("Called when there are unhandled invitations.")]
        public FsmEvent onShowUnhandledInvitations;

        [Tooltip("Called when there is no more unhandled invitations.")]
        public FsmEvent onClose;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            unhandledInvitations.ObjectType = typeof(UnityEngine.Object);
        }

        public override void OnEnter()
        {
            int tempLength = unhandledInvitations != null ? unhandledInvitations.Values.Length : 0;
            if (tempLength > 0)
            {
                unhandledInvitations.ObjectType = typeof(UnityEngine.Object);
                unhandledInvitations.SetType(VariableType.Object);
                MultiplayerInvitationObject temp = (MultiplayerInvitationObject)unhandledInvitations.Values[tempLength - 1];
                invitationObject.Value = temp;
                unhandledInvitations.Resize(tempLength - 1);
                RectTransform cg = invitationPanel.Value.GetComponent<RectTransform>();
                cg.localScale = new Vector3(0, 0, 0);
                Fsm.Event(eventTarget, onShowUnhandledInvitations);
            }
            else
            {
                RectTransform cg = invitationPanel.Value.GetComponent<RectTransform>();
                cg.localScale = new Vector3(1, 1, 1);
                invitationPanel.Value.SetActive(false);
                Fsm.Event(eventTarget, onClose);

            }


        }


    }
}

#endif