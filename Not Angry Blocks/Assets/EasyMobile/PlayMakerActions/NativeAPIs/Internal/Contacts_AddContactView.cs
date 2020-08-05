#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using EasyMobile.Internal;
using System.Collections.Generic;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions.Internal
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Add Contact to Contact View")]
    public class Contacts_AddContactView : FsmStateAction
    {
        [Tooltip("Contact")]
        public FsmObject contactObj;

        [Tooltip("Contact DemoUtil")]
        public FsmGameObject demoUtilObj;

        public override void Reset()
        {
            contactObj = null;
            demoUtilObj = null;
        }

        public override void OnEnter()
        {
            ContactObject temp = (ContactObject)contactObj.Value;
            demoUtilObj.Value.GetComponent<DemoUtils_Contacts>().AddContactView(temp.Contact);
            Finish();
        }
    }
}
#endif