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
	[Tooltip("Removes the data pool from memory.  Does not affect the saved data pool file.")]
	public class UnloadDataPool : SSUnloaded
	{
		public override void OnEnter()
		{
			FailEventOrFinish(UnloadDataPool(), NotLoadedEvent);
		}
	}
}
