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
	public abstract class SSFileAccess : SSAccess
	{
		[Tooltip("Call this event if the data pool file is not found.")]
		public FsmEvent FileNotFoundEvent;


		public override void Reset()
		{
			FileNotFoundEvent = null;
			base.Reset();
		}
	}
}
