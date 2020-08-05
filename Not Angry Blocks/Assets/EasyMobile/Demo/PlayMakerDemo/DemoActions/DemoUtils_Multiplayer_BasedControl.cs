using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Demo
{
    public abstract class DemoUtils_Multiplayer_BasedControl : MonoBehaviour
    {
        [SerializeField]
        protected DemoUtils demoUtils = null;

        [SerializeField]
        private GameObject invitationPanel = null;

        protected bool ShouldShowInvitationPanel { get; set; }

        [SerializeField]
        protected LoadingPanelController loadingPanel = null;

        protected virtual void Awake()
        {
            ShouldShowInvitationPanel = true;
            invitationPanel.SetActive(false);
        }

        public string GetInvitationDisplayString(Invitation invitation)
        {
            if (invitation == null)
                return "null\n";

            string result = "[Invitation]\n";
            result += "InvitationType: " + invitation.InvitationType + "\n";
            result += "Variant: " + invitation.Variant + "\n";
            result += "Inviter: " + GetParticipantDisplayString(invitation.Inviter);

            return result;
        }

        public string GetParticipantDisplayString(Participant participant)
        {
            if (participant == null)
                return "null\n";

            string result = "[Participant]\n";
            result += "DisplayName: " + participant.DisplayName + "\n";
            result += "IsConnectedToRoom: " + participant.IsConnectedToRoom + "\n";
            result += "ParticipantId: " + participant.ParticipantId + "\n";
            result += "Status: " + participant.Status + "\n";

            if (participant.Player == null)
            {
                result += "Player: null\n";
            }
            else
            {
                result += "Player.id: " + participant.Player.id + "\n";
                result += "Player.isFriend: " + participant.Player.isFriend + "\n";
                result += "Player.state: " + participant.Player.state + "\n";
                result += "Player.userName: " + participant.Player.userName + "\n";
            }

            return result;
        }
    }
}