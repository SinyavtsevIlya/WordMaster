#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Client.SaveLoad
{
    public class SaveLoadEditorHelpers
    {
        private const string MenuItemRoot = "Tools/Saves/";
        
        [MenuItem(MenuItemRoot + "Clear Saves")]
        public static void ClearSaves()
        {
            var saveDirectory = new DirectoryInfo(Application.persistentDataPath);
            foreach (var file in saveDirectory.GetFiles())
                file.Delete();
        }

        [MenuItem(MenuItemRoot + "Open Saves Folder")]
        public static void OpenSavesFolder()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
    }
}
#endif