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
	[Tooltip("Sets the name of the current data pool.")]
	public class SetDataPoolName : SSUnloaded
	{
		[Tooltip("The new name for the current data pool.")]
		public FsmString PoolName;


		public override void Reset()
		{
			PoolName = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			bool result;
			if (_dataPool != null)
			{
				_dataPool.Name = PoolName.Value;
				result = true;
			}
			else
				result = false;
			FailEventOrFinish(result, NotLoadedEvent);
		}
	}
}
