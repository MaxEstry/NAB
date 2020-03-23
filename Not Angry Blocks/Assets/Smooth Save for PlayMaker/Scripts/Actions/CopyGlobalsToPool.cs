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
	[Tooltip("Copies variables from the Globals to the data pool.")]
	public class CopyGlobalsToPool : SSSaveAll
	{
		public override void OnEnter()
		{
			_saveFsmVariables = FsmVariables.GlobalVariables;
			base.OnEnter();
		}
	}
}
