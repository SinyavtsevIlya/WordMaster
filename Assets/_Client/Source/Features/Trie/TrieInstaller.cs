using System;
using System.IO;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class TrieInstaller : Installer<TrieInstaller>
    {
        public override void InstallBindings()
        {
            var filname = "russian_nouns.txt";
            
            var filePath = Path.Combine(Application.dataPath, 
                "_Client", "Content", "Common" , "Vocabulary", "Russian", filname);
            
            var trie = new Trie();
            
            try
            {
                using var reader = new StreamReader(filePath);
                
                while (reader.ReadLine() is { } line)
                {
                    if (!string.IsNullOrWhiteSpace(line)) 
                        trie.Insert(line);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            Container.BindInstance(trie).AsSingle();
        }
    }
}