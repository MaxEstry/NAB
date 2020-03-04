#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Multiplayer")]
    [Tooltip("Gets the connected participants, including self.")]
    public class GameServices_RealTime_GetConnectedParticipants : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("The number of participants found.")]
        [UIHint(UIHint.Variable)]
        public FsmInt participantCount;

        [Tooltip("The Unity Object containing the all connected participants")]
        [ArrayEditor(VariableType.Object)]
        public FsmArray participants;

        public override void Reset()
        {
            base.Reset();
            participantCount = 0;
            participants = null;
        }

        public override void OnEnter()
        {
            Participant[] tParticipants = GameServices.RealTime.GetConnectedParticipants().ToArray();
           
            participantCount.Value = tParticipants.Length;

            participants.Resize(participantCount.Value);
            
            for (int i = 0; i < participantCount.Value; i++)
            {
                ParticipantObject temp = new ParticipantObject();

                temp.ParticipantData = tParticipants[i];

                if (tParticipants[i].Player != null)
                {
                    temp.Image = tParticipants[i].Player.image;
                }

                participants.Set(i, temp);        
            }
            Finish();
        }
    }
}

#endif