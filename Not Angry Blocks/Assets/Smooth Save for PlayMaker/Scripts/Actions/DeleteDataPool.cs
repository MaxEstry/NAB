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
	[Tooltip("Deletes a data pool file and, if loaded, removes it from memory.")]
	public class DeleteDataPool : SSFileAccess
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("If given, deletes the pool with this ID.  If None, the default pool is deleted.")]
		public FsmInt PoolId = -1;


		public override void Reset()
		{
			PoolId = -1;
			base.Reset();
		}


		public override void OnEnter()
		{
			FailEventOrFinish(DeleteDataPool(PoolId.Value), FileNotFoundEvent, FileErrorEvent);
		}
	}
}
