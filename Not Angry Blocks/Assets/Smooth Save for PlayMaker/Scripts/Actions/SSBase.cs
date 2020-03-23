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


using ChristopherCreates.SmoothSave;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class SSBase : FsmStateAction
	{
		protected static SmoothSaveDataPool _dataPool;
		public static bool UsePlayerPrefs { get; protected set; }
		public static bool EncodePlayerPrefs { get; protected set; }

		static readonly VariableType[] _validFsmVariableTypes =
		{
			VariableType.Bool,
			VariableType.Color,
			VariableType.Enum,
			VariableType.Float,
			VariableType.GameObject,
			VariableType.Int,
			VariableType.Quaternion,
			VariableType.Rect,
			VariableType.String,
			VariableType.Vector2,
			VariableType.Vector3
		};


		// Find matching signature in data pool
		SmoothSaveDataSource FindDataSource(SmoothSaveDataSource newDataSource)
		{
			// Check for empy data pool
			if (_dataPool == null)
				return null;
			else if (_dataPool.DataSources == null)
				return null;
			else if (_dataPool.DataSources.Count == 0)
				return null;

			// Check each data source
			foreach (var currentDataSource in _dataPool.DataSources)
				if (currentDataSource.MatchSignature(newDataSource))
					return currentDataSource;
			return null;
		}


		// Perform an action with an FSM variable
		protected bool DoVariableAction(VariableAction variableAction, NamedVariable fsmVariable, string key, bool? isGlobal = null)
		{
			// Check data
			if (fsmVariable == null)
				throw new ArgumentNullException("fsmVariable", "Cannot do action with null variable: " + variableAction);

			// If no current pool, get one
			if (_dataPool == null)
				DefaultDataPool();

			// Get global state
			if (isGlobal == null)
				isGlobal = FsmVariables.GlobalVariables.Contains(fsmVariable);

			// If GameObject, do action and exit
			if (fsmVariable.VariableType == VariableType.GameObject)
				return DoGameObjectAction(variableAction, (FsmGameObject)fsmVariable, key, (bool)isGlobal);
			// If array of GameObjects, do actions and exit
			if (fsmVariable.VariableType == VariableType.Array)
				if (((FsmArray)fsmVariable).ElementType == VariableType.GameObject)
				{
					var allSuccess = true;
					for (int index = 0; index < ((FsmArray)fsmVariable).Values.Length; index++)
						if (!DoGameObjectAction(variableAction, new FsmGameObject(fsmVariable.Name) { Value = (GameObject)((FsmArray)fsmVariable).Values[index] }, key, (bool)isGlobal, index))
							allSuccess = false;
					return allSuccess;
				}

			// Search data pool for new data source
			var newDataSource = new SmoothSaveDataSource(fsmVariable, key, (bool)isGlobal, Fsm);
			var oldDataSource = FindDataSource(newDataSource);

			// Copy variable to pool
			if (variableAction == VariableAction.CopyVariableToPool)
			{
				// If found, remove old copy
				if (oldDataSource != null)
					_dataPool.DataSources.Remove(oldDataSource);
				_dataPool.DataSources.Add(newDataSource);
				return true;
			}
			// Copy pool to variable
			else if (variableAction == VariableAction.CopyPoolToVariable)
			{
				if (oldDataSource != null)
				{
					// If no FSM variable, set it
					if (oldDataSource.GetFsmVariable() == null)
						oldDataSource.SetFsmVariable(newDataSource);
					oldDataSource.Deserialize();
					return true;
				}
				else
				{
					Debug.Log("Variable data not found in pool: " + fsmVariable.Name, Owner);
					return false;
				}
			}
			else
				throw new ArgumentException("Invalid variable action: " + variableAction);
		}


		// Perform an action with a batch of FSM variables
		protected bool DoBatchVariableAction(FsmVariables fsmVariables, VariableAction variableAction, string filter, FsmVariableType fsmVariableType)
		{
			// Check data
			if (fsmVariables == null)
				throw new ArgumentNullException("fsmVariables", "Cannot do action with null FSM variable batch: " + variableAction);

			NamedVariable[] fsmVariableSubset;
			switch (fsmVariableType)
			{
				case FsmVariableType.All:
					fsmVariableSubset = fsmVariables.GetAllNamedVariables();
					break;
				case FsmVariableType.Bool:
					fsmVariableSubset = fsmVariables.BoolVariables;
					break;
				case FsmVariableType.Color:
					fsmVariableSubset = fsmVariables.ColorVariables;
					break;
				case FsmVariableType.Float:
					fsmVariableSubset = fsmVariables.FloatVariables;
					break;
				case FsmVariableType.GameObject:
					fsmVariableSubset = fsmVariables.GameObjectVariables;
					break;
				case FsmVariableType.Int:
					fsmVariableSubset = fsmVariables.IntVariables;
					break;
				case FsmVariableType.Quaternion:
					fsmVariableSubset = fsmVariables.QuaternionVariables;
					break;
				case FsmVariableType.Rect:
					fsmVariableSubset = fsmVariables.RectVariables;
					break;
				case FsmVariableType.String:
					fsmVariableSubset = fsmVariables.StringVariables;
					break;
				case FsmVariableType.Vector2:
					fsmVariableSubset = fsmVariables.Vector2Variables;
					break;
				case FsmVariableType.Vector3:
					fsmVariableSubset = fsmVariables.Vector3Variables;
					break;
				case FsmVariableType.Array:
					fsmVariableSubset = fsmVariables.ArrayVariables;
					break;
				case FsmVariableType.Enum:
					fsmVariableSubset = fsmVariables.EnumVariables;
					break;
				default:
					throw new NotSupportedException("Invalid FSM variable type: " + fsmVariableType);
			}

			// Process each FSM variable
			var allComplete = true;
			foreach (var currentVariable in fsmVariableSubset)
				if (MatchFilter(currentVariable.Name, filter) && IsValidFsmVariableType(currentVariable))
					if (!DoVariableAction(variableAction, currentVariable, null))
						allComplete = false;
			return allComplete;
		}


		// Match a string with a filter
		bool MatchFilter(string target, string filter)
		{
			// If empty target, no match
			if (string.IsNullOrEmpty(target))
				return false;

			// If empty filter, match
			if (string.IsNullOrEmpty(filter))
				return true;

			// Make case insensitive
			target = target.ToLower();
			filter = filter.ToLower();

			// Search with start and end * wildcard
			if (filter.StartsWith("*"))
			{
				if (filter.EndsWith("*"))
					return target.Contains(filter.Trim('*'));
				else
					return target.EndsWith(filter.Trim('*'));
			}
			else
			{
				if (filter.EndsWith("*"))
					return target.StartsWith(filter.Trim('*'));
				else
					return target.Contains(filter);
			}
		}


		// Process a GameObject variable action
		bool DoGameObjectAction(VariableAction variableAction, FsmGameObject fsmGameObject, string key, bool isGlobal, int index = 0)
		{
			// Check for empty GameObject
			if (fsmGameObject.Value == null)
			{
				Debug.Log("Cannot save or load null GameObject reference: " + fsmGameObject.Name, Owner);
				return false;
			}
			if (variableAction == VariableAction.CopyVariableToPool)
				return GameObjectToPool(fsmGameObject, key, isGlobal, index);
			else if (variableAction == VariableAction.CopyPoolToVariable)
			{
				var success = PoolToGameObject(fsmGameObject, key, isGlobal, index);
				if (success)
					return true;
				else
				{
					Debug.Log("Variable data not found in pool: " + fsmGameObject.Name, Owner);
					return false;
				}
			}
			else
				throw new ArgumentException("variableAction", "Invalid variable action: " + variableAction);
		}


		// Return GameObject property tag-amended key
		string GetGameObjectPropertyKey(string key, string tag)
		{
			if (!string.IsNullOrEmpty(key))
				return key + tag;
			else
				return null;
		}


		// Copy GameObject properties to pool
		bool GameObjectToPool(FsmGameObject fsmGameObject, string key, bool isGlobal, int index = 0)
		{
			var allComplete = true;

			/*
			var fsmBoolName = fsmGameObject.Name + GameObjectPropertyTag.Active + index;
			var fsmBool = new FsmBool(fsmBoolName);
			fsmBool.Value = fsmGameObject.Value.activeSelf;
			var propertyKey = GetGameObjectPropertyKey(key, GameObjectPropertyTag.Active);
			DoVariableAction(VariableAction.CopyVariableToPool, fsmBool, propertyKey, isGlobal);
			*/

			// Active
			if (!DoVariableAction(VariableAction.CopyVariableToPool, new FsmBool(fsmGameObject.Name + GameObjectPropertyTag.Active + index) { Value = fsmGameObject.Value.activeSelf }, GetGameObjectPropertyKey(key, GameObjectPropertyTag.Active), isGlobal))
				allComplete = false;
			// Layer
			if (!DoVariableAction(VariableAction.CopyVariableToPool, new FsmInt(fsmGameObject.Name + GameObjectPropertyTag.Layer + index) { Value = fsmGameObject.Value.layer }, GetGameObjectPropertyKey(key, GameObjectPropertyTag.Layer), isGlobal))
				allComplete = false;
			// Name
			if (!DoVariableAction(VariableAction.CopyVariableToPool, new FsmString(fsmGameObject.Name + GameObjectPropertyTag.Name + index) { Value = fsmGameObject.Value.name }, GetGameObjectPropertyKey(key, GameObjectPropertyTag.Name), isGlobal))
				allComplete = false;
			// Tag
			if (!DoVariableAction(VariableAction.CopyVariableToPool, new FsmString(fsmGameObject.Name + GameObjectPropertyTag.Tag + index) { Value = fsmGameObject.Value.tag }, GetGameObjectPropertyKey(key, GameObjectPropertyTag.Tag), isGlobal))
				allComplete = false;
			// Position
			if (!DoVariableAction(VariableAction.CopyVariableToPool, new FsmVector3(fsmGameObject.Name + GameObjectPropertyTag.Position + index) { Value = fsmGameObject.Value.transform.position }, GetGameObjectPropertyKey(key, GameObjectPropertyTag.Position), isGlobal))
				allComplete = false;
			// Rotation
			if (!DoVariableAction(VariableAction.CopyVariableToPool, new FsmQuaternion(fsmGameObject.Name + GameObjectPropertyTag.Rotation + index) { Value = fsmGameObject.Value.transform.rotation }, GetGameObjectPropertyKey(key, GameObjectPropertyTag.Rotation), isGlobal))
				allComplete = false;
			// Scale
			if (!DoVariableAction(VariableAction.CopyVariableToPool, new FsmVector3(fsmGameObject.Name + GameObjectPropertyTag.Scale + index) { Value = fsmGameObject.Value.transform.localScale }, GetGameObjectPropertyKey(key, GameObjectPropertyTag.Scale), isGlobal))
				allComplete = false;

			return allComplete;
		}


		// Copy GameObject properties to pool
		bool PoolToGameObject(FsmGameObject fsmGameObject, string key, bool isGlobal, int index = 0)
		{
			var allComplete = true;
			SmoothSaveDataSource currentDataSource;

			/*
			var fsmBoolName = fsmGameObject.Name + GameObjectPropertyTag.Active;
			var fsmBool = new FsmBool(fsmBoolName);
			var propertyKey = GetGameObjectPropertyKey(key, GameObjectPropertyTag.Active);
			var newDataSource = new SmoothSaveDataSource(fsmBool, propertyKey, isGlobal, Fsm);
			currentDataSource = FindDataSource(newDataSource);
			*/

			// Active
			currentDataSource = FindDataSource(new SmoothSaveDataSource(new FsmBool(fsmGameObject.Name + GameObjectPropertyTag.Active + index), GetGameObjectPropertyKey(key, GameObjectPropertyTag.Active), isGlobal, Fsm));
			if (currentDataSource != null)
				fsmGameObject.Value.SetActive((bool)currentDataSource.Deserialize());
			else
				allComplete = false;
			// Layer
			currentDataSource = FindDataSource(new SmoothSaveDataSource(new FsmInt(fsmGameObject.Name + GameObjectPropertyTag.Layer + index), GetGameObjectPropertyKey(key, GameObjectPropertyTag.Layer), isGlobal, Fsm));
			if (currentDataSource != null)
				fsmGameObject.Value.layer = (int)currentDataSource.Deserialize();
			else
				allComplete = false;
			// Name
			currentDataSource = FindDataSource(new SmoothSaveDataSource(new FsmString(fsmGameObject.Name + GameObjectPropertyTag.Name + index), GetGameObjectPropertyKey(key, GameObjectPropertyTag.Name), isGlobal, Fsm));
			if (currentDataSource != null)
				fsmGameObject.Value.name = (string)currentDataSource.Deserialize();
			else
				allComplete = false;
			// Tag
			currentDataSource = FindDataSource(new SmoothSaveDataSource(new FsmString(fsmGameObject.Name + GameObjectPropertyTag.Tag + index), GetGameObjectPropertyKey(key, GameObjectPropertyTag.Tag), isGlobal, Fsm));
			if (currentDataSource != null)
				fsmGameObject.Value.tag = (string)currentDataSource.Deserialize();
			else
				allComplete = false;
			// Position
			currentDataSource = FindDataSource(new SmoothSaveDataSource(new FsmVector3(fsmGameObject.Name + GameObjectPropertyTag.Position + index), GetGameObjectPropertyKey(key, GameObjectPropertyTag.Position), isGlobal, Fsm));
			if (currentDataSource != null)
				fsmGameObject.Value.transform.position = (Vector3)currentDataSource.Deserialize();
			else
				allComplete = false;
			// Rotation
			currentDataSource = FindDataSource(new SmoothSaveDataSource(new FsmQuaternion(fsmGameObject.Name + GameObjectPropertyTag.Rotation + index), GetGameObjectPropertyKey(key, GameObjectPropertyTag.Rotation), isGlobal, Fsm));
			if (currentDataSource != null)
				fsmGameObject.Value.transform.rotation = (Quaternion)currentDataSource.Deserialize();
			else
				allComplete = false;
			// Scale
			currentDataSource = FindDataSource(new SmoothSaveDataSource(new FsmVector3(fsmGameObject.Name + GameObjectPropertyTag.Scale + index), GetGameObjectPropertyKey(key, GameObjectPropertyTag.Scale), isGlobal, Fsm));
			if (currentDataSource != null)
				fsmGameObject.Value.transform.localScale = (Vector3)currentDataSource.Deserialize();
			else
				allComplete = false;

			return allComplete;
		}


		// Load or create default data pool
		void DefaultDataPool()
		{
			var id = SmoothSaveDataPool.FindFirstFileId();
			if (id != -1)
				LoadDataPool(id, true);
			else
				MakeDataPool();

			if (_dataPool == null)
				throw new InvalidOperationException("Cannot create default data pool.");
		}


		// Get default pool ID
		int GetDefaultPoolId()
		{
			if (_dataPool != null)
				return _dataPool.Id;
			else
				return SmoothSaveDataPool.FindFirstFileId();
		}


		// Save the current data pool
		protected FileActionResult SaveDataPool()
		{
			if (_dataPool != null)
			{
				if (_dataPool.Save())
					return FileActionResult.Success;
				else
					return FileActionResult.AccessError;
			}
			else
				return FileActionResult.NotFound;
		}


		// Load a data pool
		public FileActionResult LoadDataPool(int id, bool ignoreEmpty = false)
		{
			// Get pool to load
			if (id == -1)
				id = GetDefaultPoolId();
			if (id == -1)
				return FileActionResult.NotFound;

			// Load the pool
			try
			{
				_dataPool = new SmoothSaveDataPool(id, ignoreEmpty);
			}
			catch (FileNotFoundException)
			{
				return FileActionResult.NotFound;
			}
			catch (Exception)
			{
				return FileActionResult.AccessError;
			}
			return FileActionResult.Success;
		}


		// Create a new data pool
		protected int MakeDataPool(string name = null)
		{
			try
			{
				_dataPool = new SmoothSaveDataPool(name);
			}
			catch (Exception exception)
			{
				Debug.LogError("Error creating data pool", Owner);
				Debug.LogError(exception);
				return -1;
			}
			return _dataPool.Id;
		}


		// Deletes a data pool file and, if loaded, removes it from memory
		protected FileActionResult DeleteDataPool(int id = -1)
		{
			// Get pool to delete
			if (id == -1)
				id = GetDefaultPoolId();
			if (id == -1)
				return FileActionResult.NotFound;
			var filePath = SmoothSaveDataPool.GetFilePath(id);

			// If current, unload
			if (_dataPool != null)
				if (id == _dataPool.Id)
					UnloadDataPool();

			// Delete data pool
			// From file
			if (!UsePlayerPrefs)
			{
				if (!File.Exists(filePath))
					return FileActionResult.NotFound;
				try
				{
					File.Delete(filePath);
				}
				catch (Exception exception)
				{
					Debug.LogWarning(exception);
					return FileActionResult.AccessError;
				}
			}
			// From PlayerPrefs
			else
			{
				if (PlayerPrefs.HasKey(filePath))
					PlayerPrefs.DeleteKey(filePath);
				else
					return FileActionResult.NotFound;
			}

			return FileActionResult.Success;
		}


		// Debug data source pool
		protected void DebugDataPool()
		{
			// Check for empty pool
			int dataPoolCount;
			if (_dataPool == null)
			{
				Debug.Log("Debug Data Pool: No data pool loaded");
				return;
			}
			else if (_dataPool.DataSources == null)
				dataPoolCount = 0;
			else
				dataPoolCount = _dataPool.DataSources.Count;

			Debug.Log("Data Pool - ID: " + _dataPool.Id + ",  Name: " + _dataPool.Name + ", Size: " + dataPoolCount);

			// Check if data exists
			if (dataPoolCount == 0)
			{
				Debug.Log("Data pool empty, nothing to debug");
				return;
			}

			// For each data
			string rawDataString;
			foreach (var currentData in _dataPool.DataSources)
			{
				// Display the info
				rawDataString = string.Join(", ", Array.ConvertAll(currentData.SerializedValue, element => element.ToString()));
				Debug.Log(string.Format("Variable: {0}, Type: {1}, FSM: {2}, Owner: {3}, Scene: {4}, Global: {5}, Array: {6}, Key: {7}, Data: {8}",
				currentData.FsmVariableName,
				currentData.FsmVariableType,
				currentData.FsmName,
				currentData.OwnerName,
				currentData.SceneName,
				currentData.IsGlobal,
				currentData.IsArray,
				currentData.Key,
				rawDataString));
			}
		}


		// Removes the data pool from memory
		protected bool UnloadDataPool()
		{
			if (_dataPool != null)
			{
				_dataPool = null;
				return true;
			}
			else
				return false;
		}


		// Check array type
		protected string CheckArrayType(FsmArray array)
		{
			if (array.ElementType == VariableType.Unknown)
				return null;
			if (_validFsmVariableTypes.Contains(array.ElementType))
				return null;

			return "Invalid array type: " + array.ElementType;
		}


		// Validate FSM variable type
		protected bool IsValidFsmVariableType(NamedVariable fsmVariable)
		{
			if (fsmVariable.VariableType == VariableType.Array)
				if (_validFsmVariableTypes.Contains(((FsmArray)fsmVariable).ElementType))
					return true;
				else
					return false;

			if (_validFsmVariableTypes.Contains(fsmVariable.VariableType))
				return true;
			else
				return false;
		}


		// Call fail event or finish
		protected void FailEventOrFinish(bool success, FsmEvent failEvent)
		{
			if (!success && failEvent != null)
				Fsm.Event(failEvent);
			else
				Finish();
		}

		protected void FailEventOrFinish(FileActionResult fileActionResult, FsmEvent notFoundEvent, FsmEvent accessErrorEvent)
		{
			if (fileActionResult == FileActionResult.NotFound && notFoundEvent != null)
			{
				Fsm.Event(notFoundEvent);
				return;
			}
			else if (fileActionResult == FileActionResult.AccessError && accessErrorEvent != null)
			{
				Fsm.Event(accessErrorEvent);
				return;
			}
			else
				Finish();
		}
	}
}
