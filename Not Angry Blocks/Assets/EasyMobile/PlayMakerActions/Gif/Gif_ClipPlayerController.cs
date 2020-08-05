#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Plays, pauses, resumes or stops the specified clip player.")]
    public class Gif_ClipPlayerController : FsmStateAction
    {
        public enum PlayerType
        {
            ClipPlayer,
            ClipPlayerUI
        }

        public enum ActionType
        {
            Play,
            Pause,
            Resume,
            Stop
        }

        [ActionSection("Clip Player")]

        [Tooltip("Select the player type.")]
        public PlayerType playerType;

        [Tooltip("The clip player.")]
        public ClipPlayer clipPlayer;

        [Tooltip("The clip player (used in UI Canvas).")]
        public ClipPlayerUI clipPlayerUI;

        [ActionSection("Control Action")]

        [Tooltip("The action to apply on the player.")]
        public ActionType actionType;

        [ActionSection("Play Params")]

        [Tooltip("The animated clip to play. Use an AnimatedClipProxy variable for this parameter.")]
        [RequiredField]
        [ObjectType(typeof(AnimatedClipProxy))]
        [UIHint(UIHint.Variable)]
        public FsmObject clip;

        [Tooltip("Optional delay before the playing starts.")]
        public FsmFloat startDelay;

        [Tooltip("If set to True, the playing loops indefinitely.")]
        public FsmBool loop;

        [Tooltip("The interface we use to interact with the actual player.")]
        protected IClipPlayer _player;

        public override void Reset()
        {
            playerType = PlayerType.ClipPlayerUI;
            clipPlayer = null;
            clipPlayerUI = null;

            actionType = ActionType.Play;

            clip = null;
            startDelay = 0;
            loop = true;
        }

        public override void OnEnter()
        {
            _player = playerType == PlayerType.ClipPlayer ? (IClipPlayer)clipPlayer : (IClipPlayer)clipPlayerUI;

            switch (actionType)
            {
                case ActionType.Play:
                    var proxyClip = (AnimatedClipProxy)clip.Value;
                    Gif.PlayClip(_player, proxyClip.clip, startDelay.Value, loop.Value);
                    break;
                case ActionType.Pause:
                    Gif.PausePlayer(_player);
                    break;
                case ActionType.Resume:
                    Gif.ResumePlayer(_player);
                    break;
                case ActionType.Stop:
                    Gif.StopPlayer(_player);
                    break;
            }

            Finish();
        }
    }
}

#endif
