using UnityEngine;

namespace WordMaster
{
    public class Alphabet
    {
        private readonly string _alphabet;

        public Alphabet(string alphabet)
        {
            _alphabet = alphabet;
        }
        
        public char[] Values => _alphabet.ToCharArray();

        public char RandomValue => _alphabet[Random.Range(0, _alphabet.Length)];
    }
}