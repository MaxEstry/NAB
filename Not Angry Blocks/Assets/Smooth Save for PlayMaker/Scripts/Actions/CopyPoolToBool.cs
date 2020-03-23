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
	[Tooltip("Copies data from the pool to a Bool variable.")]
	public class CopyPoolToBool : SSKeyData
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy data from the pool to this variable.")]
		public FsmBool Bool;


		public override void Reset()
		{
			Bool = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			FailEventOrFinish(DoVariableAction(VariableAction.CopyPoolToVariable, Bool, Key.Value), DataNotFoundEvent);
		}
	}
}
