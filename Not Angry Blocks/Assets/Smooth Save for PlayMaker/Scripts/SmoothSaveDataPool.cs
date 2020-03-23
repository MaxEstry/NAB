/*
 *  Smooth Save for PlayMaker
 *
 *  Copyright 2015 Christopher Stanley
 *
 *  Documentation: "Smooth Save Manual.pdf"
 *
 *  Support: support@ChristopherCreates.com
 */


using HutongGames.PlayMaker.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace ChristopherCreates.SmoothSave
{
	[Serializable]
	public class SmoothSaveDataPool
	{
		public int Id { get; private set; }
		public List<SmoothSaveDataSource> DataSources { get; set; }
		public string FilePath { get; private set; }
		public string Name { get; set; }

		[NonSerialized]
		const string FileNameBase = "SmoothSave";
		[NonSerialized]
		const int FileIdStringSize = 4;
		[NonSerialized]
		const int PlayerPrefsSearchSize = 10;


		// Deserialized from XML
		public SmoothSaveDataPool()
		{
		}

		// New data pool
		public SmoothSaveDataPool(string name = null)
		{
			Name = name;
			Id = GetNewId();
			if (Id == -1)
				throw new InvalidOperationException("Maximum data pools exceeded");
			DataSources = new List<SmoothSaveDataSource>();
			FilePath = GetFilePath(Id);
			if (!Save(true))
				throw new InvalidOperationException("Cannot create data pool");
		}

		// Existing data pool
		public SmoothSaveDataPool(int id, bool ignoreEmpty = false)
		{
			// Check data
			if (id < 0)
				throw new ArgumentOutOfRangeException("Id must be 0 or greater.  Id: " + id);

			// Assign properties
			Id = id;
			FilePath = GetFilePath(Id);

			// Load data pool
			SmoothSaveDataPool dataPool;
			try
			{
				// From file
				if (!SSBase.UsePlayerPrefs)
				{
					var binaryFormatter = new BinaryFormatter();
					using (var fileStream = File.OpenRead(FilePath))
						dataPool = (SmoothSaveDataPool)binaryFormatter.Deserialize(fileStream);
				}
				// From PlayerPrefs
				else
				{
					string saveData = PlayerPrefs.GetString(FilePath);
					// If flagged, decode from Base64
					if (SSBase.EncodePlayerPrefs)
						saveData = Encoding.UTF8.GetString(Convert.FromBase64String(saveData));
					var xmlSerializer = new XmlSerializer(typeof(SmoothSaveDataPool));
					using (var stringReader = new StringReader(saveData))
						dataPool = (SmoothSaveDataPool)xmlSerializer.Deserialize(stringReader);
				}
			}
			// File not found
			catch(FileNotFoundException)
			{
				Debug.LogWarning("Data pool file not found: " + FilePath);
				throw;
			}
			// Any other error
			catch (Exception exception)
			{
				Debug.LogError("Cannot load data pool file.  System error accessing file or file data corrupted: " + FilePath);
				Debug.LogError(exception);
				throw;
			}

			// Verify ID
			if (Id != dataPool.Id)
				throw new InvalidOperationException("Data pool ID mismatch. Specified ID: " + Id + ", File ID: " + dataPool.Id);

			// Copy values
			Name = dataPool.Name;
			DataSources = dataPool.DataSources;

			// Check data pool
			if (DataSources.Count == 0 && !ignoreEmpty)
				Debug.LogWarning("Loaded empty data pool.  Id: " + Id + ", Name: " + Name);
		}


		// Find first saved data pool ID
		public static int FindFirstFileId()
		{
			var id = -1;

			// Get potential data pool files
			// From files
			if (!SSBase.UsePlayerPrefs)
			{
				var indexMask = new string('?', FileIdStringSize);
				var dataPoolFiles = Directory.GetFiles(Application.persistentDataPath, FileNameBase + indexMask);
				if (dataPoolFiles.Length == 0)
					return id;
				Array.Sort(dataPoolFiles);

				// Find the first with a valid id
				foreach (var currentFile in dataPoolFiles)
					if (int.TryParse(currentFile.Substring(currentFile.Length - FileIdStringSize, FileIdStringSize), out id))
						break;
			}
			// From PlayerPrefs
			else
			{
				string result;
				for (var index = 0; index < PlayerPrefsSearchSize; index++)
				{
					result = PlayerPrefs.GetString(FileNameBase + index.ToString("D" + FileIdStringSize));
					if (result != "")
					{
						id = index;
						break;
					}
				}
			}

			return id;
        }


		// Get first available ID
		static int GetNewId()
		{
			var id = 0;
			var maxId = Math.Pow(10, FileIdStringSize);

			// Set ID check method
			Func<string,bool> checkIdMethod;
			// From file
			if (!SSBase.UsePlayerPrefs)
				checkIdMethod = File.Exists;
			// From PlayerPrefs
			else
				checkIdMethod = PlayerPrefs.HasKey;

			// Try each pool starting at ID 0
			while (checkIdMethod(GetFilePath(id)))
			{
				id++;
				if (id == maxId)
					return -1;
			}
			return id;
		}


		// Get file path from ID
		public static string GetFilePath(int id)
		{
			var baseAndIdName = FileNameBase + id.ToString("D" + FileIdStringSize);
			// From file
			if (!SSBase.UsePlayerPrefs)
				return Path.Combine(Application.persistentDataPath, baseAndIdName);
			// From PlayerPrefs
			else
				return baseAndIdName;
        }


		// Save data pool to file
		public bool Save(bool ignoreEmpty = false)
		{
			// Check for empty data pool
			if (!ignoreEmpty)
			{
				bool emptyPool = false;
				if (DataSources == null)
					emptyPool = true;
				else if (DataSources.Count == 0)
					emptyPool = true;
				if (emptyPool)
					Debug.LogWarning("Saving empty data pool.  Id: " + Id + ", Name: " + Name);
			}

			// Write data pool
			// To file
			if (!SSBase.UsePlayerPrefs)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				try
				{
					using (FileStream fileStream = File.Create(FilePath))
						binaryFormatter.Serialize(fileStream, this);
				}
				catch (Exception exception)
				{
					Debug.LogError("Error saving data pool.  Id: " + Id + ", Name: " + Name);
					Debug.LogError(exception);
					return false;
				}
			}
			// To PlayerPrefs
			else
			{
				var xmlSerializer = new XmlSerializer(typeof(SmoothSaveDataPool));
				using (var stringWriter = new StringWriter())
				{
					xmlSerializer.Serialize(stringWriter, this);
					string saveData = stringWriter.ToString();
					// If flagged, encode to Base64
					if (SSBase.EncodePlayerPrefs)
						saveData = Convert.ToBase64String(Encoding.UTF8.GetBytes(saveData));
					PlayerPrefs.SetString(FilePath, saveData);
				}
			}

			return true;
		}
	}
}
