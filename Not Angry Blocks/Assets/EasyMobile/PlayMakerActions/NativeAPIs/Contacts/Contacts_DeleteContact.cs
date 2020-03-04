#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Delete a contact from the device.")]
    public class Contacts_DeleteContact: FsmStateAction
    {
        [Tooltip("Contact's object")]
        public FsmObject contactObj;

        [ActionSection("Result")]

        [Tooltip("Event sent if delete contact successfully.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if delete contact not successfully.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        public override void Reset()
        {
            base.Reset();
            contactObj = null;
        }

        public override void OnEnter()
        {

            ContactObject temp = (ContactObject)contactObj.Value;
            Contact contact = temp.Contact;
            string error = DeviceContacts.DeleteContact(contact);

            if (string.IsNullOrEmpty(error))
            {
                Fsm.Event(eventTarget, isSuccessEvent);
            }
            else
            {
                Fsm.Event(eventTarget, isNotSuccessEvent);
            }
        }
       
    }
}
#endif