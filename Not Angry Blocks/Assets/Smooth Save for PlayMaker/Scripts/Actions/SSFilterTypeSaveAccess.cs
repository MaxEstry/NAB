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
	public abstract class SSFilterTypeSaveAccess : SSAccess
	{
		[Tooltip("If given, selects variables by name.  \"abc*\" for starts with, \"*abc\" for ends with, or \"abc\" for anywhere.  Not case sensitive.  Leave blank to select all variables.")]
		public FsmString Filter;

		[Tooltip("The type of variables to copy from.")]
		public FsmVariableType VariableType = FsmVariableType.All;

		[Tooltip("Save the data pool to file after copying from the variables.")]
		public FsmBool SaveNow = true;


		public override void Reset()
		{
			Filter = null;
			VariableType = FsmVariableType.All;
			SaveNow = true;
			base.Reset();
		}
	}
}
