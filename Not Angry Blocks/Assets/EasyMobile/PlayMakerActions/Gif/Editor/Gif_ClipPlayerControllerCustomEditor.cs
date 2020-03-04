#if PLAYMAKER
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMakerEditor;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Gif_ClipPlayerController))]
    public class Gif_ClipPlayerControllerCustomEditor : CustomActionEditor
    {
        Gif_ClipPlayerController _action;

        public override void OnEnable()
        {
            _action = (Gif_ClipPlayerController)target;
        }

        public override bool OnGUI()
        {
            EditField("playerType");

            switch (_action.playerType)
            {
                case Gif_ClipPlayerController.PlayerType.ClipPlayer:
                    EditField("clipPlayer");
                    break;
                case Gif_ClipPlayerController.PlayerType.ClipPlayerUI:
                    EditField("clipPlayerUI");
                    break;
            }

            EditField("actionType");

            if (_action.actionType == Gif_ClipPlayerController.ActionType.Play)
            {
                EditField("clip");
                EditField("startDelay");
                EditField("loop");
            }

            return GUI.changed;
        }
    }
}
#endif