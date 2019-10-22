// (c) Eric Vander Wal, 2017 All rights reserved.
// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using TMPro;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro Shader")]
	[Tooltip("Set Text Mesh Pro face shaders.")]

	public class  setTextmeshProShaderPropertiesFace : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(TextMeshPro))]
		[Tooltip("Textmesh Pro component is required.")]
		public FsmOwnerDefault gameObject;

        [ActionSection("Color")]
        public FsmColor faceColor;

        [ActionSection("Texture")]
        public FsmTexture texture;

        [ActionSection("Settings")]
        public FsmVector2 textureTiling;
        public FsmVector2 textureOffset;

        [ActionSection("Speed")]

        [HasFloatSlider(-5, 5)]
        public FsmFloat speedX;

        [HasFloatSlider(-5, 5)]
        public FsmFloat speedY;

        [ActionSection("Extra Settings")]
        [HasFloatSlider(0, 1)]
        public FsmFloat softness;

        [HasFloatSlider(-1, 1)]
        public FsmFloat dilate;
        
        [Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;

		TextMeshPro meshproScript;

		public override void Reset()
		{

			gameObject = null;
			faceColor = null;
            softness = null;
            dilate = null;
            speedX = null;
            speedY = null;
            texture = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);


			meshproScript = go.GetComponent<TextMeshPro>();

			if (!everyFrame.Value)
			{
				DoMeshChange();
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				DoMeshChange();
			}
		}

		void DoMeshChange()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			meshproScript.fontSharedMaterial.SetColor("_FaceColor", faceColor.Value);
            meshproScript.fontSharedMaterial.SetFloat("_FaceDilate", dilate.Value);
            meshproScript.fontSharedMaterial.SetFloat("_OutlineSoftness", softness.Value);
            meshproScript.fontSharedMaterial.SetFloat("_FaceUVSpeedX", speedX.Value);
            meshproScript.fontSharedMaterial.SetFloat("_FaceUVSpeedY", speedY.Value);
            meshproScript.fontSharedMaterial.SetTexture("_FaceTex", texture.Value);
            meshproScript.fontSharedMaterial.SetTextureOffset("_FaceTex", textureOffset.Value); 
            meshproScript.fontSharedMaterial.SetTextureScale("_FaceTex", textureTiling.Value);



        }

    }
}