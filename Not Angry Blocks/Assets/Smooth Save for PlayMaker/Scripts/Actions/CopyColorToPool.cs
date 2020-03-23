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
	[Tooltip("Copies a Color variable to the data pool.")]
	public class CopyColorToPool : SSKey
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy this variable to the data pool.")]
		public FsmColor Color;


		public override void Reset()
		{
			Color = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			DoVariableAction(VariableAction.CopyVariableToPool, Color, Key.Value);
			Finish();
		}
	}
}
