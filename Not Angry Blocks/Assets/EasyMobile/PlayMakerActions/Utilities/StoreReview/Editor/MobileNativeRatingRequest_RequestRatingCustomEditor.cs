#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;
using System;

namespace EasyMobile.PlayerMaker.Editor
{
    namespace EasyMobile.PlayerMaker.Actions
    {
        [CustomActionEditor(typeof(MobileNativeRatingRequest_RequestRating))]
        public class MobileNativeRatingRequest_RequestRatingCustomEditor : CustomActionEditor
        {
            MobileNativeRatingRequest_RequestRating _action;

            public override bool OnGUI()
            {
                _action = (MobileNativeRatingRequest_RequestRating)target;

                EditField("popupBehavior");

                switch (_action.popupBehavior)
                {
                    case MobileNativeRatingRequest_RequestRating.RequestRatingBehaviorOption.DefaultBehavior:
                        break;
                    case MobileNativeRatingRequest_RequestRating.RequestRatingBehaviorOption.CustomBehavior:
                        EditField("onCompleteEvent");
                        EditField("userAction");
                        break;
                }

                return GUI.changed;
            }
        }
    }
}
#endif
