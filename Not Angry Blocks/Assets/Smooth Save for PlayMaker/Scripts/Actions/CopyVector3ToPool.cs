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
	[Tooltip("Copies a Vector3 variable to the data pool.")]
	public class CopyVector3ToPool : SSKey
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy this variable to the data pool.")]
		public FsmVector3 Vector3;


		public override void Reset()
		{
			Vector3 = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			DoVariableAction(VariableAction.CopyVariableToPool, Vector3, Key.Value);
			Finish();
		}
	}
}
