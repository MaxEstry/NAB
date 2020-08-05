#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMakerEditor;
using System;
using EasyMobile.PlayerMaker.Actions;

namespace EasyMobile.PlayerMaker.Editor
{
    [CustomActionEditor(typeof(Gif_UploadToGiphy))]
    public class Giphy_UploadCustomEditor : CustomActionEditor
    {
        Gif_UploadToGiphy _action;

        public override void OnEnable()
        {
            _action = (Gif_UploadToGiphy)target;
        }

        public override bool OnGUI()
        {
            // Credentials
            EditField("useGiphyBetaKey");

            if (!_action.useGiphyBetaKey.Value)
            {
                EditField("giphyUsername");
                EditField("giphyApiKey");
            }

            // Content
            EditField("imageSource");

            if (_action.imageSource == Gif_UploadToGiphy.ImageSource.Local)
                EditField("localImagePath");
            else
                EditField("remoteImageUrl");

            EditField("tags");
            EditField("sourcePostUrl");
            EditField("isHidden");

            // Result
            EditField("progress");
            EditField("gifUrl");
            EditField("errorMessage");
            EditField("uploadingEvent");
            EditField("uploadCompletedEvent");
            EditField("uploadFailedEvent");

            return GUI.changed;
        }
    }
}
#endif