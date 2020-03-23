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
	public class CopyPoolToEnum : SSKeyData
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy data from the pool to this variable.")]
		public FsmEnum Enum;


		public override void Reset()
		{
			Enum = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			FailEventOrFinish(DoVariableAction(VariableAction.CopyPoolToVariable, Enum, Key.Value), DataNotFoundEvent);
		}
	}
}
