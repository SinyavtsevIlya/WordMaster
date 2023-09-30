using System.Collections.Generic;
using UnityEngine;

namespace WordMaster
{
    public class Trie
    {
        public SystemLanguage Language { get; }
        
        private readonly TrieNode _root = new TrieNode();

        public Trie(SystemLanguage language)
        {
            Language = language;
        }

        public void Insert(string word)
        {
            var node = _root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c))
                    node.Children[c] = new TrieNode();
                
                node = node.Children[c];
            }
            node.IsEndOfWord = true;
        }

        public bool Search(string word, out List<char> variants, out bool isEndOfTheWord)
        {
            variants = null;
            isEndOfTheWord = false;
            
            var node = _root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c))
                    return false;
                
                node = node.Children[c];
            }

            variants = new List<char>();
            
            foreach (var child in node.Children) 
                variants.Add(child.Key);

            isEndOfTheWord = node.IsEndOfWord;
            
            return true;
        }
    }

    class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
        public bool IsEndOfWord { get; set; }
    }
}