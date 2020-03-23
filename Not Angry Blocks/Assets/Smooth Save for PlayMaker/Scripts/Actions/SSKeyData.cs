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
	public abstract class SSKeyData : SSBase
	{
		[Tooltip("If given, used to identify the data.  If blank, automatic identifiers are used.")]
		public FsmString Key;

		[Tooltip("Call this event if the variable data is not found in the pool.")]
		public FsmEvent DataNotFoundEvent;


		public override void Reset()
		{
			Key = null;
			DataNotFoundEvent = null;
		}
	}
}
