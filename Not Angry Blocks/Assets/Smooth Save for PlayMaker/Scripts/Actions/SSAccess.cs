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
	public abstract class SSAccess : SSBase
	{
		[Tooltip("Call this event if there is an error accessing the data pool file.")]
		public FsmEvent FileErrorEvent;


		public override void Reset()
		{
			FileErrorEvent = null;
		}
	}
}
