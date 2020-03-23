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
	[Tooltip("Copies a Float variable to the data pool.")]
	public class CopyFloatToPool : SSKey
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy this variable to the data pool.")]
		public FsmFloat Float;


		public override void Reset()
		{
			Float = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			DoVariableAction(VariableAction.CopyVariableToPool, Float, Key.Value);
			Finish();
		}
	}
}
