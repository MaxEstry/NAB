/*
*  Smooth Save for PlayMaker
*
*  Copyright 2015 Christopher Stanley
*
*  Documentation: "Smooth Save Manual.pdf"
*
*  Support: support@ChristopherCreates.com
*/


using ChristopherCreates.SmoothSave;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class SSLoadAll : SSFilterTypeDataNowLoadFileAccess
	{
		protected FsmVariables _loadFsmVariables;

		public override void OnEnter()
		{
			if (LoadNow.Value)
			{
				var loadResult = LoadDataPool(PoolId.Value);
				if (loadResult != FileActionResult.Success)
				{
					FailEventOrFinish(loadResult, FileNotFoundEvent, FileErrorEvent);
					return;
				}
			}
			FailEventOrFinish(DoBatchVariableAction(_loadFsmVariables, VariableAction.CopyPoolToVariable, Filter.Value, VariableType), DataNotFoundEvent);
		}
	}
}
