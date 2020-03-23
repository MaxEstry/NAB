// PM 1.8
/*
 *  Smooth Save for PlayMaker
 *
 *  Copyright 2015 Christopher Stanley
 *
 *  Documentation: "Smooth Save Manual.pdf"
 *
 *  Support: support@ChristopherCreates.com
 */
 

using HutongGames.PlayMaker;
using System;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_4_5 && !UNITY_4_6 && !UNITY_4_7 && !UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2
using UnityEngine.SceneManagement;
#endif

namespace ChristopherCreates.SmoothSave
{
	[Serializable]
	public class SmoothSaveDataSource
	{
		public VariableType FsmVariableType { get; set; }
		public string Key { get; set; }
		public string FsmVariableName { get; set; }
		public bool IsGlobal { get; set; }
		public bool IsArray { get; set; }
		public string SceneName { get; set; }
		public string OwnerName { get; set; }
		public string FsmName { get; set; }
		public object[] SerializedValue { get; set; }

		[NonSerialized]
		NamedVariable _fsmVariable;

		[NonSerialized]
		static readonly Dictionary<VariableType, int> _dataTypeSizes = new Dictionary<VariableType, int>
		{
			{ VariableType.Bool, 1 },
            { VariableType.Color, 4 },
			{ VariableType.Enum, 1 },
			{ VariableType.Float, 1 },
			{ VariableType.Int, 1 },
			{ VariableType.Quaternion, 4 },
			{ VariableType.Rect, 4 },
			{ VariableType.String, 1 },
			{ VariableType.Vector2, 2 },
			{ VariableType.Vector3, 3 }
		};


		// Deserialized from XML
		public SmoothSaveDataSource()
		{
		}

		public SmoothSaveDataSource(NamedVariable fsmVariable, string key, bool isGlobal, Fsm fsm)
		{
			/*
			Variables have three categories of identification
				Type	ID
				Keyed	Variable Type + Array + Key
				Global	Variable Type + Array + Variable Name + Global
				Local	Variable Type + Array + Variable Name + Local + Scene + Owner + FSM
			*/

			// Check common data
			if (fsmVariable == null)
				throw new ArgumentNullException("fsmVariable", "Cannot create data source for null FSM variable");

			// Work around RawValue bug for array variables, case 1364
			fsmVariable.ToString();

			// Assign common properties
			_fsmVariable = fsmVariable;
			Key = StringEmptyToNull(key);
			if (_fsmVariable.VariableType == VariableType.Array)
			{
				IsArray = true;
				var realType = FsmUtility.GetVariableRealType(((FsmArray)_fsmVariable).ElementType);
				if (realType == typeof(bool))
					FsmVariableType = VariableType.Bool;
				else if (realType == typeof(Color))
					FsmVariableType = VariableType.Color;
				else if (realType == typeof(Enum))
					FsmVariableType = VariableType.Enum;
				else if (realType == typeof(float))
					FsmVariableType = VariableType.Float;
				else if (realType == typeof(int))
					FsmVariableType = VariableType.Int;
				else if (realType == typeof(Quaternion))
					FsmVariableType = VariableType.Quaternion;
				else if (realType == typeof(Rect))
					FsmVariableType = VariableType.Rect;
				else if (realType == typeof(string))
					FsmVariableType = VariableType.String;
				else if (realType == typeof(Vector2))
					FsmVariableType = VariableType.Vector2;
				else if (realType == typeof(Vector3))
					FsmVariableType = VariableType.Vector3;
				else
					throw new NotSupportedException("Invalid FSM variable type: " + realType);
			}
			else
				FsmVariableType = fsmVariable.VariableType;

			// If keyed, serialize and exit
			if (Key != null)
			{
				Serialize();
				return;
			}

			// Assign global and local properties
			IsGlobal = isGlobal;
			FsmVariableName = fsmVariable.Name;

			// Check global and local data
			if (string.IsNullOrEmpty(FsmVariableName))
				throw new ArgumentNullException("fsmVariable.Name", "Cannot create global or FSM data source with null or empty FSM variable name");

			// If global, serialize and exit
			if (IsGlobal)
			{
				Serialize();
				return;
			}

			// Check local data
			if (fsm == null)
				throw new ArgumentNullException("fsm", "Cannot create FSM data source with null or empty FSM");

			// Assign local properties
#if UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
			SceneName = StringEmptyToNull(Application.loadedLevelName);
#else
			SceneName = StringEmptyToNull(SceneManager.GetActiveScene().name);
#endif
			OwnerName = StringEmptyToNull(fsm.OwnerName);
			FsmName = StringEmptyToNull(fsm.Name);

			// Check local data
			if (SceneName == null)
				throw new NotSupportedException("Cannot create FSM data source without loaded scene");
			if (OwnerName == null)
				throw new ArgumentNullException("fsm.OwnerName", "Cannot create FSM data source with null or empty owner name");
			if (FsmName == null)
				throw new ArgumentNullException("fsm.Name", "Cannot create FSM data source with null or empty FSM name");

			// Serialize and exit
			Serialize();
		}


		// Make variable from saved data
		public bool MatchSignature(SmoothSaveDataSource dataSource, bool logMismatch = false)
		{
			// Check data
			if (dataSource == null)
				throw new ArgumentNullException("dataSource", "Cannot match signature of null data source");

			// Check each property
			string errorMessage = null;

			if (FsmVariableType != dataSource.FsmVariableType)
				errorMessage = "Variable type mismatch";
			else if (Key != dataSource.Key)
				errorMessage = "Key mismatch";
			else if (FsmVariableName != dataSource.FsmVariableName)
				errorMessage = "Variable name mismatch";
			else if (IsArray != dataSource.IsArray)
				errorMessage = "Array mismatch";
			else if (IsGlobal != dataSource.IsGlobal)
				errorMessage = "Global mismatch";
			else if (SceneName != dataSource.SceneName)
				errorMessage = "Scene name mismatch";
			else if (OwnerName != dataSource.OwnerName)
				errorMessage = "Owner name mismatch";
			else if (FsmName != dataSource.FsmName)
				errorMessage = "FSM name mismatch";

			// Test mismatch
			if (errorMessage != null)
			{
				if (logMismatch)
					Debug.LogWarning(errorMessage);
				return false;
			}

			// It's a match!
			return true;
		}


		// Set data source
		public void SetFsmVariable(SmoothSaveDataSource dataSource)
		{
			// Check data source
			if (dataSource == null)
				throw new ArgumentNullException("dataSource", "Cannot set FSM variable from null data source");
			if (FsmVariableType != dataSource.FsmVariableType)
				throw new ArgumentException("Cannot set FSM variable from data source of different type. Old type: " + FsmVariableType + ", New Type: " + dataSource.FsmVariableType);

			// Get FSM variable
			var fsmVariable = dataSource.GetFsmVariable();
			if (fsmVariable == null)
				throw new ArgumentNullException("dataSource.GetFsmVariable()", "Cannot set FSM variable from data source with null FSM variable");
			_fsmVariable = fsmVariable;
		}


		// Get FSM variable
		public NamedVariable GetFsmVariable()
		{
			return _fsmVariable;
		}


		// Break data source into an object array
		public void Serialize()
		{
			// Check data
			if (_fsmVariable == null)
				throw new InvalidOperationException("Cannot serialize null FSM variable");
			if (_fsmVariable.RawValue == null)
				throw new InvalidOperationException("Cannot serialize unassigned FSM variable");

			// Serialize
			var serializedList = new List<object>();
			if (IsArray)
				foreach (var element in (object[])_fsmVariable.RawValue)
					serializedList.AddRange(BreakValue(element));
			else
				serializedList.AddRange(BreakValue(_fsmVariable.RawValue));
			SerializedValue = serializedList.ToArray();
		}


		// Break value into an object array
		object[] BreakValue(object value)
		{
			switch (FsmVariableType)
			{
				case VariableType.Bool:
				case VariableType.Enum:
				case VariableType.Float:
				case VariableType.Int:
				case VariableType.String:
					return new object[] { value };
				case VariableType.Color:
					if (value == null)
						return new object[] { null, null, null, null };
					else
					{
						var color = (Color)value;
						return new object[] { color.r, color.g, color.b, color.a };
					}
				case VariableType.Quaternion:
					if (value == null)
						return new object[] { null, null, null, null };
					else
					{
						var quaternion = (Quaternion)value;
						return new object[] { quaternion.x, quaternion.y, quaternion.z, quaternion.w };
					}
				case VariableType.Rect:
					if (value == null)
						return new object[] { null, null, null, null };
					else
					{
						var rect = (Rect)value;
						return new object[] { rect.x, rect.y, rect.width, rect.height };
					}
				case VariableType.Vector2:
					if (value == null)
						return new object[] { null, null };
					else
					{
						var vector2 = (Vector2)value;
						return new object[] { vector2.x, vector2.y };
					}
				case VariableType.Vector3:
					if (value == null)
						return new object[] { null, null, null };
					else
					{
						var vector3 = (Vector3)value;
						return new object[] { vector3.x, vector3.y, vector3.z };
					}
				default:
					throw new NotSupportedException("Invalid FSM variable type: " + FsmVariableType);
			}
		}


		// Make data source from an object array
		public object Deserialize()
		{
			// Check data
			if (SerializedValue == null)
				throw new InvalidOperationException("Cannot deserialize null data");
			if (IsArray)
			{
				if (SerializedValue.Length % _dataTypeSizes[FsmVariableType] != 0)
					throw new InvalidOperationException("Data size does not match data type.  Array data length of " + SerializedValue.Length + " is not evenly divisible by type size of " + _dataTypeSizes[FsmVariableType]);
			}
			else
				if (SerializedValue.Length != _dataTypeSizes[FsmVariableType])
					throw new InvalidOperationException("Data size does not match data type.  Expected size: " + _dataTypeSizes[FsmVariableType] + ", Actual size: " + SerializedValue.Length);

			// Deserialize data
			var serializedQueue = new Queue<object>(SerializedValue);
			var ElementValues = new List<object>();
			var ArrayValues = new List<object>();
			while (serializedQueue.Count > 0)
			{
				ElementValues.Clear();
				for (int chunk = 0; chunk < _dataTypeSizes[FsmVariableType]; chunk++)
					ElementValues.Add(serializedQueue.Dequeue());
				ArrayValues.Add(MakeValue(ElementValues.ToArray()));
			}

			// Set value(s)
			object deserializedValue;
			if (IsArray)
				deserializedValue = ArrayValues.ToArray();
			else
				deserializedValue = ArrayValues[0];
			if (_fsmVariable != null)
				_fsmVariable.RawValue = deserializedValue;
			return deserializedValue;
        }


		// Make value from an object array
		object MakeValue(object[] parts)
		{
			// If null, nothing to do
			if (parts == null)
				return null;

			// If basic type, return it
			switch (FsmVariableType)
			{
				case VariableType.Bool:
				case VariableType.Enum:
				case VariableType.Float:
				case VariableType.Int:
				case VariableType.String:
					return parts[0];
			}

			// Build complex types
			var floats = Array.ConvertAll(parts, element => (float)element);
			switch (FsmVariableType)
			{
				case VariableType.Color:
					return new Color(floats[0], floats[1], floats[2], floats[3]);
				case VariableType.Quaternion:
					return new Quaternion(floats[0], floats[1], floats[2], floats[3]);
				case VariableType.Rect:
					return new Rect(floats[0], floats[1], floats[2], floats[3]);
				case VariableType.Vector2:
					return new Vector2(floats[0], floats[1]);
				case VariableType.Vector3:
					return new Vector3(floats[0], floats[1], floats[2]);
				default:
					throw new NotSupportedException("Invalid FSM variable type: " + FsmVariableType);
			}
		}


		// Convert empty string to null
		static string StringEmptyToNull(string target)
		{
			if (target == "")
				return null;
			else
				return target;
		}
	}
}
