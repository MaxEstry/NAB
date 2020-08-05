#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions.Internal
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Get a contact from contact view")]
    public class DemoUtils_GetContactFromView : FsmStateAction
    {
        [Tooltip("The contact view prefab.")]
        public FsmOwnerDefault gameObject;

        [ActionSection("Result")]

        [Tooltip("The Object contain contact")]
        public FsmObject contactObj;

        [Tooltip("Contact Id")]
        public FsmString contactId;


        public override void Reset()
        {
            base.Reset();
            gameObject = null;
            contactObj = null;
        }

        public override void OnEnter()
        {
            Contact contact = gameObject.GameObject.Value.GetComponentInParent<ContactView>().GetContact();

            ContactObject tempObj = new ContactObject();
            tempObj.Contact = contact;
            contactObj.Value = tempObj;
            contactId.Value = contact.Id;
            Finish();

        }
    }
}

#endif