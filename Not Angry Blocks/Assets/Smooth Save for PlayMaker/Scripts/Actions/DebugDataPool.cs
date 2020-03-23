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
	[Tooltip("Displays the contents of the data pool on the console.")]
	public class DebugDataPool : SSBase
	{
		[UIHint(UIHint.Description)]
		public string description = "Displays the contents of the data pool on the console.";


		public override void OnEnter()
		{
			DebugDataPool();
			Finish();
		}
	}
}
