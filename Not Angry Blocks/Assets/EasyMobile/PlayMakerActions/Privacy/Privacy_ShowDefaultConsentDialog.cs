#if PLAYMAKER
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Privacy")]
    [Tooltip("Shows the default consent dialog that is built with the consent dialog composer in Privacy module settings UI.")]
    public class Privacy_ShowDefaultConsentDialog : FsmStateAction
    {
        [Tooltip("Whether the dialog can be dismissed.")]
        public FsmBool dismissible;

        [ActionSection("Result")]

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent when the consent dialog is dismissed.")]
        public FsmEvent dismissedEvent;

        [Tooltip("Event sent when the consent dialog is completed (the user selected an action button).")]
        public FsmEvent completedEvent;

        [Tooltip("The ID of the action button (not the dismiss button) the user used to close the dialog")]
        [UIHint(UIHint.Variable)]
        public FsmString buttonId;

        [Tooltip("The number of toggles displayed in the dialog.")]
        [UIHint(UIHint.Variable)]
        public FsmInt toggleCount;

        [Tooltip("The IDs of the toggles displayed in the dialog. " +
            "The associated value of each toggle has the same index in the 'Toggle Values' array.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.String)]
        public FsmArray toggleIDs;

        [Tooltip("The values of the toggles displayed in the dialog. " +
            "The associated ID of each toggle has the same index in the 'Toggle IDs' array.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Bool)]
        public FsmArray toggleValues;

        public override void Reset()
        {
            dismissible = false;
            eventTarget = null;
            dismissedEvent = null;
            completedEvent = null;
            buttonId = null;
            toggleCount = null;
            toggleIDs = null;
            toggleValues = null;
        }

        public override void OnEnter()
        {
            var dialog = Privacy.ShowDefaultConsentDialog(dismissible.Value);
            dialog.Dismissed += Dialog_Dismissed;
            dialog.Completed += Dialog_Completed;
        }

        void Dialog_Completed(ConsentDialog dialog, ConsentDialog.CompletedResults results)
        {
            buttonId.Value = results.buttonId;

            if (results.toggleValues != null)
            {
                toggleCount.Value = results.toggleValues.Count;
                toggleIDs.stringValues = new string[toggleCount.Value];
                toggleValues.boolValues = new bool[toggleCount.Value];

                int i = 0;

                foreach (KeyValuePair<string, bool> pair in results.toggleValues)
                {
                    toggleIDs.stringValues[i] = pair.Key;
                    toggleValues.boolValues[i] = pair.Value;
                    i++;
                }
            }

            Fsm.Event(eventTarget, completedEvent);
            Finish();
        }

        void Dialog_Dismissed(ConsentDialog obj)
        {
            Fsm.Event(eventTarget, dismissedEvent);
            Finish();
        }
    }
}
#endif

