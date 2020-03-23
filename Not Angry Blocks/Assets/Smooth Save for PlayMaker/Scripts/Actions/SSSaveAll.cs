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
	public abstract class SSSaveAll : SSFilterTypeSaveAccess
	{
		protected FsmVariables _saveFsmVariables;

		public override void OnEnter()
		{
			DoBatchVariableAction(_saveFsmVariables, VariableAction.CopyVariableToPool, Filter.Value, VariableType);
			if (SaveNow.Value)
			{
				FailEventOrFinish(SaveDataPool(), null, FileErrorEvent);
				return;
			}
			else
				Finish();
		}
	}
}
