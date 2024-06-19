namespace WordFinderResolver.Service
{
    public interface IWordFinderFactory
    {
        WordFinder CreateWordFinder(IEnumerable<string> matrix);
    }
}
