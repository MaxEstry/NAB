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
	[Tooltip("Gets information about the current data pool.")]
	public class GetDataPoolInfo : SSUnloaded
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the ID of the current data pool.  -1 if no pool is loaded.")]
		public FsmInt PoolId;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the name of the current data pool.")]
		public FsmString PoolName;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the number of values stored in the current data pool.")]
		public FsmInt PoolSize;


		public override void Reset()
		{
			PoolId = null;
			PoolName = null;
			PoolSize = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			bool result;
			if (_dataPool != null)
			{
				PoolId.Value = _dataPool.Id;
				PoolName.Value = _dataPool.Name;
				if (_dataPool.DataSources != null)
					PoolSize.Value = _dataPool.DataSources.Count;
				else
					PoolSize.Value = 0;
				result = true;
			}
			else
			{
				PoolId.Value = -1;
				PoolName.Value = null;
				PoolSize.Value = 0;
				result = false;
			}
			FailEventOrFinish(result, NotLoadedEvent);
		}
	}
}
