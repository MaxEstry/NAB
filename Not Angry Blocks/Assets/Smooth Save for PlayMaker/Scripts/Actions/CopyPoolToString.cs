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
	[Tooltip("Copies data from the pool to a String variable.")]
	public class CopyPoolToString : SSKeyData
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy data from the pool to this variable.")]
		public FsmString String;


		public override void Reset()
		{
			String = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			FailEventOrFinish(DoVariableAction(VariableAction.CopyPoolToVariable, String, Key.Value), DataNotFoundEvent);
		}
	}
}
