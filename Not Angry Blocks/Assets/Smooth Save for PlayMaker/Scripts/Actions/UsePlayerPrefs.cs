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
	[Tooltip("Instructs Smooth Save to use PlayerPrefs instead of files.  Use for platforms without a conventional file system.  See the documentation for details.")]
	public class UsePlayerPrefs : SSBase
	{
		[Tooltip("Makes the saved data cheat-resistant, but uses more memory.")]
		public FsmBool EncodeData = true;


		public override void Reset()
		{
			EncodeData = true;
		}


		public override void OnEnter()
		{
			UsePlayerPrefs = true;
			EncodePlayerPrefs = EncodeData.Value;
			Finish();
		}
	}
}
