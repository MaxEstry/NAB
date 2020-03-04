#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Get all the contacts in the devices.")]
    public class Contacts_GetContacts : FsmStateAction
    {

        [ActionSection("Result")]

        [Tooltip("Event sent if get contacts successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if get contacts not successful.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("The number of contacts found.")]
        [UIHint(UIHint.Variable)]
        public FsmInt contactCount;

        [Tooltip("The Unity Object containing the all contacts")]
        [ArrayEditor(VariableType.Object)]
        public FsmArray contacts;

        [Tooltip("Error message")]
        public FsmString errorMsg;

        public override void Reset()
        {
            base.Reset();
            contactCount = 0;
            contacts = null;
        }

        public override void OnEnter()
        {
            DeviceContacts.GetContacts(OnContactsReceived);           
        }

        void OnContactsReceived(string error, Contact[] contactsReceived)
        {
            if (string.IsNullOrEmpty(error)&&contactsReceived!=null)
            {
                contactCount.Value = contactsReceived.Length;
                contacts.Resize(contactCount.Value);

                for (int i = 0; i < contactCount.Value; i++)
                {
                    ContactObject contactObject = new ContactObject();

                    contactObject.Contact = contactsReceived[i];

                    contacts.Set(i, contactObject);
                }

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