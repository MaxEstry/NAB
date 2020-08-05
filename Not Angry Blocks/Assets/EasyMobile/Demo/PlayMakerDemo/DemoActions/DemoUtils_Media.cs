using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
    public class DemoUtils_Media : MonoBehaviour
    {

        [SerializeField]
        private uint randomTextureWidth = 512, randomTextureHeight = 256;

        [SerializeField]
        private Color[] randomTextureColors = null;

        [SerializeField]
        private Color videoBackground = Color.white;

        [SerializeField]
        private FullScreenMovieControlMode videoControlMode = FullScreenMovieControlMode.Full;

        [SerializeField]
        private FullScreenMovieScalingMode videoScalingMode = FullScreenMovieScalingMode.None;

        [SerializeField]
        private MediaResultView viewPrefab = null;

        [SerializeField]
        private Transform viewRoot = null;

        [SerializeField]
        private RawImage displayImage = null;

        [SerializeField]
        private float imagePadding = 5;

        [SerializeField]
        private RectTransform displayImageTransform = null, parentTransform = null;

        [SerializeField]
        private Dropdown imageFormatDropdown = null;

        [SerializeField]
        private Dropdown cameraTypeDropdown = null;

        private List<MediaResultView> displayedViews = new List<MediaResultView>();

        private void Start()
        {
            InitDropdownWithEnum(imageFormatDropdown, typeof(ImageFormat));
            InitDropdownWithEnum(cameraTypeDropdown, typeof(CameraType));
        }

        public void RandomDisplayImage()
        {
            UpdateDisplayImage(GenerateRandomTexture2D());
        }

        public void UpdateDisplayImage(Texture2D texture)
        {
            if (texture == null)
                return;
           
            displayImage.texture = texture;
            SizeToParent(parentTransform, displayImageTransform, displayImage, imagePadding);
        }

        public void ClearDisplayImage()
        {
            displayImage.texture = null;
        }

        private Vector2 SizeToParent(RectTransform parent, RectTransform imageTransform, RawImage image, float padding = 0)
        {
            float width = 0, height = 0;

            if (image.texture != null)
            {
                if (!parent)
                    return imageTransform.sizeDelta;

                padding = 1 - padding;
                float ratio = image.texture.width / (float)image.texture.height;
                var bounds = new Rect(0, 0, parent.rect.width, parent.rect.height);

                if (Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90)
                    bounds.size = new Vector2(bounds.height, bounds.width);

                height = bounds.height * padding;
                width = height * ratio;
                if (width > bounds.width * padding)
                {
                    width = bounds.width * padding;
                    height = width / ratio;
                }
            }

            imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            return imageTransform.sizeDelta;
        }

        private Texture2D GenerateRandomTexture2D()
        {
            Texture2D texture = new Texture2D((int)randomTextureWidth, (int)randomTextureHeight);
            Color[] colors = texture.GetPixels();

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    int index = x + y * texture.width;
                    Color color = randomTextureColors[UnityEngine.Random.Range(0, randomTextureColors.Length)];
                    colors[index] = color;
                }
            }

            texture.SetPixels(colors);
            texture.Apply();
            return texture;
        }

        private void InitDropdownWithEnum(Dropdown dropdown, System.Type enumType)
        {
            dropdown.ClearOptions();
            var options = new Dropdown.OptionDataList();

            foreach (var value in Enum.GetValues(enumType))
                options.options.Add(new Dropdown.OptionData(value.ToString()));

            dropdown.AddOptions(options.options);
            dropdown.value = 0;
        }

        public void AddViewWithError(string error)
        {
            var view = Instantiate(viewPrefab, viewRoot);
            view.gameObject.SetActive(true);
            view.UpdateWithError(error);
            displayedViews.Add(view);
        }

        public void AddView(MediaResult model)
        {
            var view = Instantiate(viewPrefab, viewRoot);
            view.VideoBackground = videoBackground;
            view.VideoControlMode = videoControlMode;
            view.VideoScalingMode = videoScalingMode;
            view.gameObject.SetActive(true);
            view.UpdateWithModel(model);
            displayedViews.Add(view);
        }

        public void RemoveDisplayedView(MediaResultView view)
        {
            if (displayedViews == null || view == null)
                return;

            Destroy(view.gameObject);
            displayedViews.Remove(view);
        }

        public void ClearDisplayedViews()
        {
            foreach (var view in displayedViews)
                Destroy(view.gameObject);
            displayedViews.Clear();
        }

    }

}