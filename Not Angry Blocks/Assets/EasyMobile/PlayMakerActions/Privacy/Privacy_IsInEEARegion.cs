#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Privacy")]
    [Tooltip("Attempts to determines whether the current device is in the European Economic Area (EEA) region.")]
    public class Privacy_IsInEEARegion : FsmStateAction
    {
        [ActionSection("Result")]

        [Tooltip("True if detected as EEA region, False otherwise.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isEEARegion;

        [Tooltip("Event sent if detected as EEA region.")]
        public FsmEvent isEEARegionEvent;

        [Tooltip("Event sent if not detected as EEA region.")]
        public FsmEvent isNotEEARegionEvent;

        public override void Reset()
        {
            isEEARegion = null;
            isNotEEARegionEvent = null;
            isEEARegionEvent = null;
        }

        public override void OnEnter()
        {
            DoMyAction();
        }

        void DoMyAction()
        {
            Privacy.IsInEEARegion(result =>
                {
                    isEEARegion.Value = result == EEARegionStatus.InEEA;

                    if (isEEARegion.Value)
                        Fsm.Event(isEEARegionEvent);
                    else
                        Fsm.Event(isNotEEARegionEvent);

                    Finish();
                });
        }
    }
}
#endif

