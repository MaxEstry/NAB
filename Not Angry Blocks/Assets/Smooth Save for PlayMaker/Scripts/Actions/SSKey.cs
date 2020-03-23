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
	public abstract class SSKey : SSBase
	{
		[Tooltip("If given, used to identify the data.  If blank, automatic identifiers are used.")]
		public FsmString Key;


		public override void Reset()
		{
			Key = null;
		}
	}
}
