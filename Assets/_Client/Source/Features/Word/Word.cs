namespace WordMaster
{
    public class Word
    {
        public string Value { get; }
        public int Score => Value.Length;
    }
}