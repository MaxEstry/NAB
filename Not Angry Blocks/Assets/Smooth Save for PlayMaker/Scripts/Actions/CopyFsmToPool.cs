/*
*  Smooth Save for PlayMaker
*
*  Copyright 2015 Christopher Stanley
*
*  Documentation: "Smooth Save Manual.pdf"
*
*  Support: support@ChristopherCreates.com
*/


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Smooth Save")]
	[Tooltip("Copies variables from the current FSM to the data pool.")]
	public class CopyFsmToPool : SSSaveAll
	{
		public override void OnEnter()
		{
			_saveFsmVariables = Fsm.Variables;
			base.OnEnter();
		}
	}
}
