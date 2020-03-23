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
	[Tooltip("Copies an Array variable to the data pool.")]
	public class CopyArrayToPool : SSKey
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy this variable to the data pool.")]
		public FsmArray Array;


		public override void Reset()
		{
			Array = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			DoVariableAction(VariableAction.CopyVariableToPool, Array, Key.Value);
			Finish();
		}


		public override string ErrorCheck()
		{
			return CheckArrayType(Array);
		}
	}
}
