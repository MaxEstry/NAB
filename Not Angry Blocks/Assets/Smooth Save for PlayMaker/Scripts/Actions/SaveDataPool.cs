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
	[Tooltip("Saves the current data pool to file.")]
	public class SaveDataPool : SSUnloadedAccess
	{
		public override void OnEnter()
		{
			FailEventOrFinish(SaveDataPool(), NotLoadedEvent, FileErrorEvent);
		}
	}
}
