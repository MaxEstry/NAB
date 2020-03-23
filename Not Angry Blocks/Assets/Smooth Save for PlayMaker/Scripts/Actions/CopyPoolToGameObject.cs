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
	[Tooltip("Copies data from the pool to a Game Object variable.")]
	public class CopyPoolToGameObject : SSKeyData
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Copy data from the pool to this variable.")]
		public FsmGameObject GameObject;


		public override void Reset()
		{
			GameObject = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			FailEventOrFinish(DoVariableAction(VariableAction.CopyPoolToVariable, GameObject, Key.Value), DataNotFoundEvent);
		}
	}
}
