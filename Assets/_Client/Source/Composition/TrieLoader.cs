using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WordMaster
{
    public class TrieLoader
    {
        public async Task<Trie> Load()
        {
            var filename = "russian_nouns.txt";
            
            var filePath = Path.Combine(Application.dataPath, 
                "_Client", "Content", "Common" , "Vocabulary", "Russian", filename);
            
            var trie = new Trie();
            
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                var www = UnityWebRequest.Get($"./{filename}");
                var completionSource = new TaskCompletionSource<string>();
                var operation = www.SendWebRequest();
                operation.completed += op => completionSource.SetResult(www.downloadHandler.text);
                
                await completionSource.Task;
                
                if (www.result == UnityWebRequest.Result.Success)
                {
                    using var reader = new StringReader(www.downloadHandler.text);
                    for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        if (!string.IsNullOrWhiteSpace(line)) 
                            trie.Insert(line);
                    }
                }
                else
                {
                    throw new Exception(www.error);
                }
            }
            else
            {
                using var reader = new StreamReader(filePath);
                
                while (reader.ReadLine() is { } line)
                {
                    if (!string.IsNullOrWhiteSpace(line)) 
                        trie.Insert(line);
                }
            }

            return trie;
        }
    }

    public class Vocabulary
    {
        public string Value;
    }
}