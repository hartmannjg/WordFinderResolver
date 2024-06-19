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

        /// <summary>
        /// Receives a list of words that must be searched in the matrix.
        /// </summary>
        /// <param name="wordstream">list of words searched.</param>
        /// <returns>List of the 10 most frequent words in the matrix ordered from highest to lowest</returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordCounts = new Dictionary<string, int>();

            foreach (var word in wordstream)
            {
                int count = 0;
                foreach (var row in _matrix)
                {
                    count += CountOccurrences(row, word);
                }
                if (count > 0)
                {
                    wordCounts[word] = count;
                }
            }

            return wordCounts
                .OrderByDescending(kvp => kvp.Value)
                .Take(10)
                .Select(kvp => kvp.Key);
        }

        /// <summary>
        /// Receives a row and word and try and search the repetitions of the word in the row.
        /// </summary>
        /// <param name="row">complete row of the matrix.</param>
        /// /// <param name="word">word searched in the row.</param>
        /// <returns>Number of times the word apears in the row.</returns>
        private int CountOccurrences(string row, string word)
        {
            int count = 0;
            int startIndex = 0;

            while ((startIndex = row.IndexOf(word, startIndex, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                count++;
                startIndex += word.Length;
            }

            return count;
        }

    }
}
