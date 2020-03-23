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
	public abstract class SSUnloadedAccess : SSAccess
	{
		[Tooltip("Called if no data pool is currently loaded.")]
		public FsmEvent NotLoadedEvent;


		public override void Reset()
		{
			NotLoadedEvent = null;
			base.Reset();
		}
	}
}
