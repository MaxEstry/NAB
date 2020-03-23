/*
*  Smooth Save for PlayMaker
*
*  Copyright 2015 Christopher Stanley
*
*  Documentation: "Smooth Save Manual.pdf"
*
*  Support: support@ChristopherCreates.com
*/


namespace ChristopherCreates.SmoothSave
{
	public static class GameObjectPropertyTag
	{
		public const string Active = ":SSGOactiveSelf";
		public const string Layer = ":SSGOlayer";
		public const string Name = ":SSGOname";
		public const string Tag = ":SSGOtag";
		public const string Position = ":SSGOtransform.position";
		public const string Rotation = ":SSGOtransform.rotation";
		public const string Scale = ":SSGOtransform.localScale";
	}

	public enum VariableAction
	{
		CopyVariableToPool,
		CopyPoolToVariable
	}

	public enum FsmVariableType
	{
		All,
		Bool,
		Color,
		Float,
		GameObject,
		Int,
		Quaternion,
		Rect,
		String,
		Vector2,
		Vector3,
		Array,
        Enum
	}

	public enum FileActionResult
	{
		Success,
		NotFound,
		AccessError
	}
}
