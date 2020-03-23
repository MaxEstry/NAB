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
	[Tooltip("Copies data from the pool to variables in the current FSM.")]
	public class CopyPoolToFsm : SSLoadAll
	{
		public override void OnEnter()
		{
			_loadFsmVariables = Fsm.Variables;
			base.OnEnter();
		}
	}
}
