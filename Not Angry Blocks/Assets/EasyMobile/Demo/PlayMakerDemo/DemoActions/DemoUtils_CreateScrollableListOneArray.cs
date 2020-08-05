#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System.Collections.Generic;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    public class DemoUtils_CreateScrollableListOneArray : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(ScrollableList))]
        public FsmGameObject listPrefab;

        [RequiredField]
        public FsmString uiTitle;

        [RequiredField]
        [ArrayEditor(VariableType.String)]
        public FsmArray values;

        [ActionSection("Result")]

        [Tooltip("The selected value")]
        [UIHint(UIHint.Variable)]
        public FsmString selectedValue;

        public override void Reset()
        {
            listPrefab = null;
            uiTitle = null;
            values = null;
            selectedValue = null;
        }

        public override void OnEnter()
        {
            if (listPrefab.Value == null)
            {
                Finish();
                return;
            }

            if (values == null || values.stringValues == null)
            {
                Finish();
                return;
            }

            var items = new Dictionary<string, string>();

            for (int i = 0; i < values.stringValues.Length; i++)
            {
                items.Add(values.stringValues[i], string.Empty);
            }

            var scrollableList = ScrollableList.Create(listPrefab.Value, uiTitle.Value, items);
            scrollableList.ItemSelected += OnScrollableListComplete;
            scrollableList.UIClosed += OnScrollableListClosed;
        }

        void OnScrollableListComplete(ScrollableList list, string title, string subtitle)
        {
            list.ItemSelected -= OnScrollableListComplete;
            selectedValue.Value = title;
            Finish();
        }

        void OnScrollableListClosed(ScrollableList list)
        {
            list.UIClosed -= OnScrollableListClosed;
            Finish();
        }
    }
}
#endif

