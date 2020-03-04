#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Open native UI to pick up contacts.")]
    public class Contacts_PickContact : FsmStateAction
    {

        [ActionSection("Result")]

        [Tooltip("Event sent if pick contact successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if pick contact not successful.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("The Unity Object containing the contact")]
        public FsmObject contactObject;

        [Tooltip("Error message")]
        public FsmString errorMsg;

        public override void Reset()
        {
            base.Reset();
            contactObject = null;
        }

        public override void OnEnter()
        {
            DeviceContacts.PickContact(OnContactReceived);           
        }

        void OnContactReceived(string error, Contact contactReceived)
        {
            if (string.IsNullOrEmpty(error)&&contactReceived!=null)
            {
                ContactObject contactObjectTemp = new ContactObject();

                contactObjectTemp.Contact = contactReceived;

                contactObject.Value = contactObjectTemp;
                Fsm.Event(eventTarget, isSuccessEvent);
            }
            else
            {
                errorMsg.Value = error;
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }

        }
    }
}
#endif