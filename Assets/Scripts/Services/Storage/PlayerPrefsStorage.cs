using UnityEngine;
using System;

namespace CompanyName.StorageService
{
  public class PlayerPrefsStorage : IStorage
  {
    public void Set<T>(string key, T value)
    {
      var type = typeof(T);

      if (type == typeof(int))
      {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
      }
      else if (type == typeof(float))
      {
        PlayerPrefs.SetFloat(key, Convert.ToSingle(value));
      }
      else if (type == typeof(string))
      {
        PlayerPrefs.SetString(key, value?.ToString() ?? "");
      }
      else if (type == typeof(bool))
      {
        PlayerPrefs.SetInt(key, Convert.ToBoolean(value) ? 1 : 0);
      }
      else
      {
        // For complex types, serialize to JSON
        string json = JsonUtility.ToJson(value);
        PlayerPrefs.SetString(key, json);
      }
    }

    public T Get<T>(string key, T defaultValue = default(T))
    {
      if (!HasKey(key))
        return defaultValue;

      var type = typeof(T);

      if (type == typeof(int))
      {
        return (T)(object)PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue));
      }
      else if (type == typeof(float))
      {
        return (T)(object)PlayerPrefs.GetFloat(key, Convert.ToSingle(defaultValue));
      }
      else if (type == typeof(string))
      {
        return (T)(object)PlayerPrefs.GetString(key, defaultValue?.ToString() ?? "");
      }
      else if (type == typeof(bool))
      {
        int boolValue = PlayerPrefs.GetInt(key, Convert.ToBoolean(defaultValue) ? 1 : 0);
        return (T)(object)(boolValue == 1);
      }
      else
      {
        // For complex types, deserialize from JSON
        try
        {
          string json = PlayerPrefs.GetString(key, "");
          if (string.IsNullOrEmpty(json))
            return defaultValue;
          return JsonUtility.FromJson<T>(json);
        }
        catch (Exception)
        {
          return defaultValue;
        }
      }
    }

    public bool HasKey(string key)
    {
      return PlayerPrefs.HasKey(key);
    }

    public void DeleteKey(string key)
    {
      PlayerPrefs.DeleteKey(key);
    }

    public void DeleteAll()
    {
      PlayerPrefs.DeleteAll();
    }

    public void Save()
    {
      PlayerPrefs.Save();
    }
  }
}