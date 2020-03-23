/*
*  Smooth Save for PlayMaker
*
*  Copyright 2015 Christopher Stanley
*
*  Documentation: "Smooth Save Manual.pdf"
*
*  Support: support@ChristopherCreates.com
*/


using ChristopherCreates.SmoothSave;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Smooth Save")]
	[Tooltip("Copies a Vector2 variable to the data pool.")]
	public class CopyVector2ToPool : SSKey
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy this variable to the data pool.")]
		public FsmVector2 Vector2;


		public override void Reset()
		{
			Vector2 = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			DoVariableAction(VariableAction.CopyVariableToPool, Vector2, Key.Value);
			Finish();
		}
	}
}
