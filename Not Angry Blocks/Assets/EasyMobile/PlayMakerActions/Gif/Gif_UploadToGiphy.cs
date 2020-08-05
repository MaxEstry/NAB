#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Gif")]
    [Tooltip("Uploads the GIF image to Giphy.")]
    public class Gif_UploadToGiphy : FsmStateAction
    {
        public enum ImageSource
        {
            Local,
            Remote
        }

        [ActionSection("Giphy Credentials")]
        [Tooltip("Check to upload using Giphy's pubic beta key, uncheck to upload to your own channel using your own username and API key.")]
        public FsmBool useGiphyBetaKey;

        [Tooltip("Your Giphy username.")]
        public FsmString giphyUsername;

        [Tooltip("Your Giphy production API key.")]
        public FsmString giphyApiKey;

        [ActionSection("Upload Content")]

        [Tooltip("The location of the GIF image to upload.")]
        public ImageSource imageSource;

        [Tooltip("The local image filepath, required if no sourceImageUrl supplied." +
            "If both localImagePath and sourceImageUrl are supplied, the local file" +
            "will be used over the sourceImageUrl.")]
        public FsmString localImagePath;

        [Tooltip("The URL for the image to be uploaded, required if no localImagePath specified." +
            "If both localImagePath and sourceImageUrl are supplied, the local file" +
            "will be used over the sourceImageUrl.")]
        public FsmString remoteImageUrl;

        [Tooltip("[Optional] Comma-delimited list of tags.")]
        public FsmString tags;

        [Tooltip("[Optional] The source of the asset.")]
        public FsmString sourcePostUrl;

        [Tooltip("If set to true, the uploaded image will be marked as private (only visible to the uploader).")]
        public FsmBool isHidden;

        [ActionSection("Result")]

        [Tooltip("Upload progress running from 0 to 1.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat progress;

        [Tooltip("The Giphy-hosted URL of the uploaded image.")]
        [UIHint(UIHint.Variable)]
        public FsmString gifUrl;

        [Tooltip("The error message in case the upload failed.")]
        [UIHint(UIHint.Variable)]
        public FsmString errorMessage;

        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Event sent repeatedly while the upload is in progress.")]
        public FsmEvent uploadingEvent;

        [Tooltip("Event sent once the upload completed.")]
        public FsmEvent uploadCompletedEvent;

        [Tooltip("Event sent when the upload failed.")]
        public FsmEvent uploadFailedEvent;

        public override void Reset()
        {
            // Credentials
            useGiphyBetaKey = true;
            giphyUsername = null;
            giphyApiKey = null;

            // Content
            imageSource = ImageSource.Local;
            localImagePath = null;
            remoteImageUrl = null;
            tags = string.Empty;
            sourcePostUrl = string.Empty;
            isHidden = false;

            // Result
            progress = null;
            gifUrl = null;
            errorMessage = null;
            eventTarget = null;
            uploadingEvent = null;
            uploadCompletedEvent = null;
            uploadFailedEvent = null;
        }

        public override void OnEnter()
        {
            var content = new GiphyUploadParams();
            content.localImagePath = localImagePath.Value;
            content.sourceImageUrl = remoteImageUrl.Value;
            content.tags = tags.Value;
            content.sourcePostUrl = sourcePostUrl.Value;
            content.isHidden = isHidden.Value;

            if (useGiphyBetaKey.Value)
                Giphy.Upload(content, UploadProgressCallback, UploadCompletedCallback, UploadFailedCallback);
            else
                Giphy.Upload(giphyUsername.Value, giphyApiKey.Value, content, UploadProgressCallback, UploadCompletedCallback, UploadFailedCallback);
        }

        void UploadProgressCallback(float p)
        {
            progress.Value = p;
            Fsm.Event(eventTarget, uploadingEvent);
        }

        void UploadCompletedCallback(string url)
        {
            gifUrl.Value = url;
            Fsm.Event(eventTarget, uploadCompletedEvent);
            Finish();
        }

        void UploadFailedCallback(string error)
        {
            errorMessage.Value = error;
            Fsm.Event(eventTarget, uploadFailedEvent);
            Finish();
        }
    }
}

#endif
