#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Contacts")]
    [Tooltip("Check if the previous GetContacts(Action{string, Contact[]}) method is still running.")]
    public class Contacts_IsFetchingContacts : FsmStateAction
    {
        [Tooltip("Repeats every frame.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [Tooltip("True if the previous GetContacts(Action{string, Contact[]}) method is still running.")]
        [UIHint(UIHint.Variable)]
        public FsmBool IsFetchingContacts;

        public override void Reset()
        {
            IsFetchingContacts = null;
            everyFrame = false;
        }


        public override void OnEnter()
        {
            DoMyAction();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoMyAction();
        }

        void DoMyAction()
        {
            IsFetchingContacts.Value = DeviceContacts.IsFetchingContacts;
        }
    }
}
#endif