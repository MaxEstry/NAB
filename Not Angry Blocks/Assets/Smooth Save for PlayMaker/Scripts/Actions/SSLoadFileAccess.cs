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
	public abstract class SSLoadFileAccess : SSFileAccess
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("If given, loads the pool with this ID.  If None, the default pool is loaded.")]
		public FsmInt PoolId = -1;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the name of the loaded data pool.")]
		public FsmString PoolName;


		public override void Reset()
		{
			PoolId = -1;
			PoolName = null;
			base.Reset();
		}
	}
}
