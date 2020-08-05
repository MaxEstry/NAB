#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions.Internal
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Delete a contact view.")]
    public class DemoUtils_DeleteContactView : FsmStateAction
    {
        [Tooltip("Contact view prefab")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Contact DemoUtil")]
        public FsmGameObject demoUtilObj;

        public override void Reset()
        {
            base.Reset();
            gameObject = null;
        }

        public override void OnEnter()
        {
            ContactView contact = gameObject.GameObject.Value.GetComponentInParent<ContactView>();

            demoUtilObj.Value.GetComponent<DemoUtils_Contacts>().DeleteContactAfter(contact);

          
            Finish();

        }
    }
}

#endif