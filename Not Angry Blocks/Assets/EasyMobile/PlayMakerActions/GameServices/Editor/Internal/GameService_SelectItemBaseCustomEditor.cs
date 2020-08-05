#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;
using EasyMobile.Editor;
using System.Collections.Generic;

namespace EasyMobile.PlayerMaker.Editor
{
    public class GameService_SelectItemBaseCustomEditor : CustomActionEditor
    {
        protected Dictionary<string, string> gsConstsDict;
        protected string[] gsConsts;

        public override void OnEnable()
        {
            //Prepare a string array of the names of existing leaderboards (and achievements).
            gsConstsDict = EM_EditorUtil.GetGameServiceConstants();
            gsConsts = new string[gsConstsDict.Count + 1];
            gsConsts[0] = EM_Constants.NoneSymbol;
            gsConstsDict.Keys.CopyTo(gsConsts, 1);
        }

        public override bool OnGUI()
        {
            return false;
        }

        protected bool EditSelectItemField(string fieldDisplayName, FsmString field)
        {
            if (gsConsts.Length == 1)
            {
                EditorGUILayout.HelpBox("No leaderboard/achievement found. Please open EasyMobile settings to create them and " +
                    "generate the GameServiceConstants class to use this action.", MessageType.Error);
            }

            EditorGUI.BeginChangeCheck();

            int currentIndex = 0;

            if (field != null)
                currentIndex = Mathf.Max(System.Array.IndexOf(gsConsts, EM_EditorUtil.GetKeyForValue(gsConstsDict, field.Value)), 0);
            else
                field = string.Empty;

            int newIndex = EditorGUILayout.Popup(fieldDisplayName, currentIndex, gsConsts);
    
            if (EditorGUI.EndChangeCheck())
            {
                // Position 0 is [None].
                if (newIndex == 0)
                {
                    field.Value = string.Empty;
                }
                else
                {
                    field.Value = gsConstsDict[gsConsts[newIndex]];
                }
            }

            return GUI.changed;
        }
    }
}
#endif