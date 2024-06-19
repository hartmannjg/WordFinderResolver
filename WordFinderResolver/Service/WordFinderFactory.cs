namespace WordFinderResolver.Service
{
    public class WordFinderFactory : IWordFinderFactory
    {
        public WordFinder CreateWordFinder(IEnumerable<string> matrix)
        {
            return new WordFinder(matrix);
        }
    }
}
