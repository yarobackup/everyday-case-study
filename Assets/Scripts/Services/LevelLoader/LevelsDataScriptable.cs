using System.Collections.Generic;
using CompanyName.Level;
using UnityEngine;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CompanyName.LevelLoaderService
{
  [CreateAssetMenu(fileName = "StreaksDataScriptable", menuName = "Scriptable Objects/StreaksDataScriptable")]
  public class StreaksDataScriptable : ScriptableObject
  {
#if UNITY_EDITOR
    [SerializeField] private bool _append = true;
    [SerializeField] private DefaultAsset _folderWithLevels;
#endif
    [SerializeField] private List<LevelDataScriptable> _levels;

    public int Count => _levels.Count;

    public LevelDataScriptable GetLevelAsset(int index)
    {
      if (index < 0)
      {
        return _levels[0];
      }
      if (index >= _levels.Count)
      {
        return _levels[_levels.Count - 1];
      }
      return _levels[index];
    }

    internal bool TryGelLevel(int streak, int index, out DailyData data)
    {
      data = null;
      if (streak >= _levels.Count)
      {
        streak = _levels.Count - 1;
      }
      if (streak < 0)
      {
        streak = 0;
      }
      data = _levels[streak].dailyData[index];
      return true;
    }

#if UNITY_EDITOR
    internal void AddLevel(LevelDataScriptable newLevel)
    {
      _levels.Add(newLevel);
      EditorUtility.SetDirty(this);
    }

    [Button]
    public bool CheckLevels()
    {
      if (_levels == null || _levels.Count == 0)
      {
        Debug.LogError("Levels are empty");
        return false;
      }
      var isAllLevelsValid = true;
      for (int i = 0; i < _levels.Count; i++)
      {
        var level = _levels[i];
        if (level == null)
        {
          Debug.LogError($"Level {i} is null");
          isAllLevelsValid = false;
          continue;
        }
        isAllLevelsValid = true;
      }
      Debug.Log("Levels are valid: " + isAllLevelsValid);
      return isAllLevelsValid;
    }

    [Button]
    public void LoadLevelsFromFolder()
    {
      if (_folderWithLevels == null)
      {
        Debug.LogError("No folder selected for levels");
        return;
      }

      var importedLevels = new List<LevelDataScriptable>();
      string folderPath = AssetDatabase.GetAssetPath(_folderWithLevels);
      string[] guids = AssetDatabase.FindAssets("t:LevelDataScriptable", new[] { folderPath });

      foreach (string guid in guids)
      {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        LevelDataScriptable level = AssetDatabase.LoadAssetAtPath<LevelDataScriptable>(path);
        if (level != null)
        {
          importedLevels.Add(level);
        }
      }

      // Sort levels by name to ensure correct order
      importedLevels.Sort((a, b) => string.Compare(a.name, b.name, System.StringComparison.Ordinal));

      if (_append)
      {
        _levels.AddRange(importedLevels);
      }
      else
      {
        _levels = importedLevels;
      }

      EditorUtility.SetDirty(this);
      AssetDatabase.SaveAssets();
    }
#endif
  }
}
