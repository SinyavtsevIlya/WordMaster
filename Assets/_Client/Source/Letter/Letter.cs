using System;
using UniRx;
using UnityEngine;
using System.Linq;

namespace WordMaster
{
    public class Letter
    {
        public char Value { get; }
        public ReactiveProperty<Vector2> Position { get; }
        public int Radius { get; }
        public CompositeDisposable Disposables { get; }
        
        public Letter(char value, Vector2 position, 
            int radius, CompositeDisposable disposables)
        {
            Value = value;
            Position = new ReactiveProperty<Vector2>(position);
            Radius = radius;
            Disposables = disposables;
        }
    }

    public class Sequence
    {
        public IReactiveCollection<Node> Value { get; }
        public IReadOnlyReactiveProperty<Node> Head => Value.ObserveAdd().Select(e => e.Value).ToReadOnlyReactiveProperty();

        public int Radius { get; }

        public CompositeDisposable Disposables { get; }
    }

    public class Node
    {

        public NodeSettings Settings { get; }

        public Letter Letter { get; }
        public ReactiveProperty<Node> Next { get; }
        
        public ReactiveProperty<Node> Prev { get; }
        
        public CompositeDisposable Disposables { get; }
        
        public Node(Letter letter, Node next)
        {
            Letter = letter ?? throw new ArgumentNullException(nameof(letter));
            Next = new ReactiveProperty<Node>(next);
        }
    }

    
    [CreateAssetMenu(menuName = "Create NodeSettings", fileName = "NodeSettings", order = 0)]
    public class NodeSettings : ScriptableObject
    {
        public float VerticalMovementSmoothness;
    }

    public class LetterSettings : ScriptableObject
    {
        public float Size;
    }

    public class Level
    {
        public LevelSettings Settings { get; }
        public IReactiveCollection<Letter> Letters { get; }
        public IReactiveCollection<Word> Words { get; }
    }
    
    [CreateAssetMenu(menuName = "Create LevelSettings", fileName = "LevelSettings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        public int CharactersCount;
    }

    public class Player
    {
        public Sequence Sequence { get; }
        public IReadOnlyReactiveCollection<Word> CompletedWords { get; }
    }

    public class Word
    {
        public string Value { get; }
        public int Score => Value.Length;
    }

    public class PickLetterRule
    {
        public PickLetterRule(Sequence sequence, Level level)
        {
            var head = sequence.Head.Value.Letter;
            
            head.Position.Subscribe(position =>
            {
                if (TryGetCollision(level, head, out var collisionLetter))
                {
                    var prevHead = sequence.Value.Last();
                    var newHead = new Node(collisionLetter, prevHead);
                    prevHead.Prev.Value = newHead;
                    
                    sequence.Value.Add(newHead);
                    level.Letters.Remove(collisionLetter);
                }
            }).AddTo(sequence.Disposables);
        }

        private static bool TryGetCollision(Level level, Letter head, out Letter collisionLetter)
        {
            foreach (var levelLetter in level.Letters)
            {
                if (IsColliding(levelLetter, head))
                {
                    collisionLetter = levelLetter;
                    return true;
                }
            }

            collisionLetter = null;
            return false;
        }

        private static bool IsColliding(Letter a, Letter b) => 
            (a.Position.Value - b.Position.Value).magnitude < a.Radius + b.Radius;
    }

    public class TailMovementRule
    {
        public TailMovementRule(Node node)
        {
            node.Prev.Subscribe(prev =>
            {
                Observable.EveryUpdate()
                    .Subscribe(_ =>
                    {
                        var position = prev.Letter.Position.Value;
                        var t = Time.deltaTime * node.Settings.VerticalMovementSmoothness;
                        var y = Mathf.Lerp(node.Letter.Position.Value.y, position.y, t);
                        node.Letter.Position.Value = new Vector2(position.x, y);
                    })
                    .AddTo(prev.Disposables);
            }).AddTo(node.Disposables);
        }
    }

    public class LetterGenerationRule
    {
        private static readonly System.Random Random = new System.Random();
        private const string Chars = "абвгдеёжзиклмнопрстуфхцчшщъыьэюя";
        
        public LetterGenerationRule(Level level)
        {
            Enumerable.Repeat(Chars, level.Settings.CharactersCount)
                .Select(s => s[Random.Next(s.Length)]).ToList()
                .ForEach(character =>
                {
                    level.Letters.Add(new Letter(character, new Vector2(Random.Next(), Random.Next()), 1, new CompositeDisposable()));               
                });
        }
    }

    public class LetterPresentRule
    {
        public LetterPresentRule(Letter letter, LetterView view)
        {
            view.SetCharacter(letter.Value);
            letter.Position.Subscribe(view.SetPosition).AddTo(letter.Disposables);
        }
    }

    public class LetterView : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text[] _textLayers;
        [SerializeField] private LetterViewSettings _settings;
        
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetCharacter(char character)
        {
            foreach (var text in _textLayers) 
                text.SetText(character.ToString());
        }

        private void Awake()
        {
            for (var index = 0; index < _textLayers.Length; index++) 
                ColorizeTextLayer(index);
        }

        private void ColorizeTextLayer(int index)
        {
            var normalizedValue = (float)index / _textLayers.Length;
            _textLayers[index].color = _settings.EvaluateColor(normalizedValue);
        }
    }

    public class LetterViewSettings : ScriptableObject
    {
        [SerializeField] private Gradient _gradient;

        public Color EvaluateColor(float normalizedValue) => _gradient.Evaluate(normalizedValue);
    }
}