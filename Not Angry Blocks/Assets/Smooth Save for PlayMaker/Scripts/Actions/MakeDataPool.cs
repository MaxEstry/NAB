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
	[Tooltip("Creates and loads a new data pool.  Unloads any previous data pool.")]
	public class MakeDataPool : SSAccess
	{
		[Tooltip("If given, sets the data pool name.")]
		public FsmString PoolName;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the ID of the created data pool.")]
		public FsmInt PoolId;


		public override void Reset()
		{
			PoolId = null;
			PoolName = null;
			base.Reset();
		}


		public override void OnEnter()
		{
			PoolId.Value = MakeDataPool(PoolName.Value);
			bool result;
			if (PoolId.Value != -1)
				result = true;
			else
				result = false;
			FailEventOrFinish(result, FileErrorEvent);
		}
	}
}
