#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using System.Collections.Generic;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    public class DemoUtils_CreateScrollableListCompoundArray : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(ScrollableList))]
        public FsmGameObject listPrefab;

        [RequiredField]
        public FsmString uiTitle;

        [RequiredField]
        [CompoundArray("Items", "Title", "Subtitle")]
        public FsmString[] titles;
        public FsmString[] subtitles;

        [ActionSection("Result")]

        [Tooltip("The selected title")]
        [UIHint(UIHint.Variable)]
        public FsmString selectedTitle;

        [Tooltip("The selected subtitle")]
        [UIHint(UIHint.Variable)]
        public FsmString selectedSubtitle;

        public override void Reset()
        {
            listPrefab = null;
            uiTitle = null;
            titles = null;
            subtitles = null;
            selectedTitle = null;
            selectedSubtitle = null;
        }

        public override void OnEnter()
        {
            if (listPrefab.Value == null)
            {
                Finish();
                return;
            }

            if (titles == null)
            {
                Finish();
                return;
            }

            var items = new Dictionary<string, string>();

            for (int i = 0; i < titles.Length; i++)
            {
                string titleStr = titles[i].Value;
                string subtitleStr = subtitles != null && subtitles[i] != null ? subtitles[i].Value : string.Empty;

                items.Add(titleStr, subtitleStr);
            }

            var scrollableList = ScrollableList.Create(listPrefab.Value, uiTitle.Value, items);
            scrollableList.ItemSelected += OnScrollableListComplete;
            scrollableList.UIClosed += OnScrollableListClosed;
        }

        void OnScrollableListComplete(ScrollableList list, string title, string subtitle)
        {
            list.ItemSelected -= OnScrollableListComplete;

            selectedTitle.Value = title;
            selectedSubtitle.Value = subtitle;
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

