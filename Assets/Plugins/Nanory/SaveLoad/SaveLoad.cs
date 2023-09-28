using System;
using System.IO;
using UnityEngine;

namespace Plugins.Nanory.SaveLoad
{
    public class SaveLoadOps : ISaveLoad
    {
        public const string Extension = ".json";
        
        public LoadInfo<TObject> Load<TObject>(string fileName)
        {
            var loadInfo = new LoadInfo<TObject>();
            try
            {
                var path = GetFullPath(fileName);
                if (File.Exists(path))
                {
                    using var reader = File.OpenText(path);
                    var content = reader.ReadToEnd();
                    loadInfo.Result = JsonUtility.FromJson<TObject>(content);
                    loadInfo.Status = LoadStatus.Success;
                }
                else
                    loadInfo.Status = LoadStatus.FileNotExist;
            }
            catch (Exception e)
            {
                loadInfo.Exception = e;
                loadInfo.Status = LoadStatus.Failed;
            }

            return loadInfo;
        }

        public void Save<TObject>(string fileName, TObject savable)
        {
            var saveInfo = new SaveInfo<TObject>();
            
            try
            {
                var path = GetFullPath(fileName);
                var json = JsonUtility.ToJson(savable);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                saveInfo.Status = SaveStatus.Failed;
                saveInfo.Exception = e;
            }
        }
        
        private static string GetFullPath(string fileName)
        {
            return $"{Path.Combine(GetRootPath(), fileName)}{Extension}";
        }
        
        private static string GetRootPath()
        {
            return Application.persistentDataPath;
        }
    }
    
    public class PlayerPrefsSaveLoad : ISaveLoad 
    {
        public LoadInfo<TObject> Load<TObject>(string fileName)
        {
            var loadInfo = new LoadInfo<TObject>();
            
            if (PlayerPrefs.HasKey(fileName))
            {
                var loadResult = PlayerPrefs.GetString(fileName);
                loadInfo.Result = JsonUtility.FromJson<TObject>(loadResult);
                loadInfo.Status = LoadStatus.Success;
            }
            else
            {
                loadInfo.Status = LoadStatus.FileNotExist;
            }

            return loadInfo;
        }

        public void Save<TObject>(string fileName, TObject savable)
        {
            var json = JsonUtility.ToJson(savable);
            PlayerPrefs.SetString(fileName, json);
            PlayerPrefs.Save();
        }
    }

    public interface ISaveLoad
    {
        LoadInfo<TObject> Load<TObject>(string fileName);
        void Save<TObject>(string fileName, TObject savable);
    }

    public struct LoadInfo<TObject>
    {
        public LoadInfo(LoadStatus status, TObject result, Exception exception = null)
        {
            Status = status;
            Result = result;
            Exception = exception;
        }

        public LoadStatus Status;
        public TObject Result;
        public Exception Exception;
    }
        
    public enum LoadStatus
    {
        None,
        Success,
        FileNotExist,
        Failed
    }

    public struct SaveInfo<TObject>
    {
        public SaveStatus Status;
        public Exception Exception;
    }
    
    public enum SaveStatus
    {
        None,
        Success,
        Failed
    }
}