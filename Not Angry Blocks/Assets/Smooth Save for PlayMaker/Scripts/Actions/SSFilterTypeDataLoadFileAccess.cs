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
	public abstract class SSFilterTypeDataNowLoadFileAccess : SSLoadFileAccess
	{
		[Tooltip("If given, selects variables by name.  \"abc*\" for starts with, \"*abc\" for ends with, or \"abc\" for anywhere.  Not case sensitive.  Leave blank to select all variables.")]
		public FsmString Filter;

		[Tooltip("The type of variables to copy to.")]
		public FsmVariableType VariableType = FsmVariableType.All;

		[Tooltip("Call this event if the variable data is not found in the pool.")]
		public FsmEvent DataNotFoundEvent;

		[Tooltip("Load the data pool from file before copying to the variables.")]
		public FsmBool LoadNow = true;


		public override void Reset()
		{
			Filter = null;
			VariableType = FsmVariableType.All;
			DataNotFoundEvent = null;
			LoadNow = true;
			base.Reset();
		}
	}
}
