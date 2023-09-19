using System.Linq;
using UnityEngine;

namespace WordMaster
{
    public class Alphabet
    {
        private readonly string _alphabet;
        private readonly string _wordBreakingLetters;

        public Alphabet(string alphabet, string wordBreakingLetters, string initialWord)
        {
            _alphabet = alphabet;
            _wordBreakingLetters = wordBreakingLetters;
            InitialWord = initialWord;
        }
        
        public char[] Values => _alphabet.ToCharArray();

        public char[] ValidStartValues => _alphabet.Except(_wordBreakingLetters.ToCharArray()).ToArray();

        public char RandomValue => ValidStartValues[Random.Range(0, ValidStartValues.Length)];

        public string InitialWord { get; }
    }
}