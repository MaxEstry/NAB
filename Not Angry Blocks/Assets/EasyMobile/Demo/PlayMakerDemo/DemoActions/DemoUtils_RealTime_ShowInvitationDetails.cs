#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Handle Received Message")]
    public class DemoUtils_RealTime_ShowInvitationDetails : FsmStateAction
    {
        [Tooltip("The Unity Object containing the invitation data")]
        public FsmObject invitationObject;

        [ActionSection("Result")]

        [Tooltip("The Output information in string")]
        public FsmString info;

        public override void Reset()
        {
            base.Reset();
            invitationObject = null;
            info = null;
        }

        public override void OnEnter()
        {
            MultiplayerInvitationObject temp = (MultiplayerInvitationObject)invitationObject.Value;
            Invitation invitation = temp.Invitation;
            info.Value = GetInvitationDisplayString(invitation);
            Finish();
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

#endif