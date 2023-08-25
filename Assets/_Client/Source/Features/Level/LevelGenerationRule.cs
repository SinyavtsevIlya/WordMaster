using System.Linq;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class LevelGenerationRule
    {
        private static readonly System.Random Random = new System.Random();
        private const string Chars = "абвгдеёжзиклмнопрстуфхцчшщъыьэюя";
        
        public LevelGenerationRule(Level level)
        {
            Enumerable.Repeat(Chars, level.Settings.CharactersCount)
                .Select(s => s[Random.Next(s.Length)]).ToList()
                .ForEach(character =>
                {
                    level.Letters.Add(new Letter(character, new Vector2(Random.Next(), Random.Next()), 1, new CompositeDisposable()));               
                });
        }
    }
}