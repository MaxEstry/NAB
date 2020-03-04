#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine.UI;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Handle Received Message")]
    public class DemoUtils_RealTime_ShowInvitationPanel : FsmStateAction
    {
        [Tooltip("check true if showing unhandled invitation")]
        public FsmBool showUnhandledInvitation;

        [Tooltip("The Unity Object containing the Input invitation data")]
        public FsmObject inputInvitationObject;

        [Tooltip("The Invitation Panel")]
        public FsmGameObject invitationPanel;

        [Tooltip("The Object containing the invitation information text")]
        public FsmGameObject invitationTextInfoObject;

        [Tooltip("The Unity Object containing the all unhandled invitations")]
        [ArrayEditor(VariableType.Object)]
        public FsmArray unhandledInvitations;

        [ActionSection("Result")]

        [Tooltip("The Unity Object containing the current invitation data")]
        public FsmObject invitationObject;



        public override void Reset()
        {
            base.Reset();
            invitationObject = null;
            invitationTextInfoObject = null;
            invitationPanel = null;
            showUnhandledInvitation = false;
            unhandledInvitations.ObjectType = typeof(UnityEngine.Object);
        }

        public override void OnEnter()
        {
            MultiplayerInvitationObject temp = (MultiplayerInvitationObject)inputInvitationObject.Value;
            Invitation invitation = temp.Invitation;

            if (showUnhandledInvitation.Value == true)
            {
                invitationObject.Value = temp;
                invitationPanel.Value.SetActive(true);
                invitationTextInfoObject.Value.GetComponent<Text>().text = "<b>Inviter:</b>\n" + (invitation.Inviter.DisplayName);
                RectTransform cg = invitationPanel.Value.GetComponent<RectTransform>();
                cg.localScale = new Vector3(1, 1, 1);

            }
            else if (invitationPanel.Value.activeSelf == true)
            {
                int tempLength = unhandledInvitations != null ? unhandledInvitations.Values.Length : 0;
                unhandledInvitations.Resize(tempLength + 1);
                unhandledInvitations.Set(tempLength, temp);
            }
            else
            {
                invitationObject.Value = inputInvitationObject.Value;
                invitationTextInfoObject.Value.GetComponent<Text>().text = "<b>Inviter:</b>\n" + (invitation.Inviter.DisplayName);
                invitationPanel.Value.SetActive(true);
                RectTransform cg = invitationPanel.Value.GetComponent<RectTransform>();
                cg.localScale = new Vector3(1, 1, 1);
            }
            Finish();

        }


    }
}

#endif