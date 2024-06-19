using System.Net;
using System.Security;
using WordFinderResolver.Dto;

namespace WordFinderResolver.Service
{
    public class WordFinderService
    {
        private readonly WordFinderFactory _wordFinderFactory;

        public WordFinderService(WordFinderFactory wordFinderFactory) 
        { 
            _wordFinderFactory = wordFinderFactory;
        }
        public async Task<IEnumerable<string>> Resolve(MatrixColecctionDto dto) 
        {
            try
            {
                var matrixList = await ConvertMatrixToEnumerable(dto.Matrix);

                var wordFinder = _wordFinderFactory.CreateWordFinder(matrixList);

                throw new NotImplementedException();
            }
            catch (Exception) 
            {
                throw;
            }

        }

        private Task<IEnumerable<string>> ConvertMatrixToEnumerable(string[,] matrix)
        {
            List<string> result = new List<string>();

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            //Add words from rows
            for (int i = 0; i < rowCount; i++)
            {
                string wordResult = "";
                for (int j = 0; j < colCount; j++)
                {
                    wordResult += matrix[i, j];
                }
                result.Add(wordResult);
            }

            //Add words from columns
            for (int j = 0; j < colCount; j++)
            {
                string wordResult = "";
                for (int i = 0; i < rowCount; i++)
                {
                    wordResult += matrix[i, j];
                }
                result.Add(wordResult);
            }


            IEnumerable<string> enumerableResult = result;

            return Task.FromResult<IEnumerable<string>>(enumerableResult);
        }
    }
}
