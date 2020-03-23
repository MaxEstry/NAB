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
	[Tooltip("Copies data from the pool to variables in the Globals.")]
	public class CopyPoolToGlobals : SSLoadAll
	{
		public override void OnEnter()
		{
			_loadFsmVariables = FsmVariables.GlobalVariables;
			base.OnEnter();
		}
	}
}
