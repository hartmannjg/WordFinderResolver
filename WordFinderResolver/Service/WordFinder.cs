namespace WordFinderResolver.Service
{
    public class WordFinder : IWordFinder
    {
        private readonly IEnumerable<string> _matrix;
        public WordFinder(IEnumerable<string> matrix)
        {         

            if(matrix.Count() > 64 || matrix.Any(x => x.Length > 64))
            {
                throw new Exception("Wrong size of the matrix");
            }
            _matrix = matrix;
        }
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            
        }
    }
}
