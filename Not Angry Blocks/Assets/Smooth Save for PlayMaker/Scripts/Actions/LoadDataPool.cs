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
	[Tooltip("Loads a data pool from file.")]
	public class LoadDataPool : SSLoadFileAccess
	{
		public override void OnEnter()
		{
			FailEventOrFinish(LoadDataPool(PoolId.Value), FileNotFoundEvent, FileErrorEvent);
		}
	}
}
