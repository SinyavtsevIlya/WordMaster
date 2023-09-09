using System;
using System.IO;
using UnityEngine;

namespace Plugins.Nanory.SaveLoad
{
    public static class SaveLoadOps
    {
        public const string Extension = ".json";
        
        public static LoadInfo<TObject> Load<TObject>(string fileName)
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

        public static void Save<TObject>(string fileName, TObject savable, Action<SaveInfo<TObject>> onCompleted)
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
            
            onCompleted.Invoke(saveInfo);
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