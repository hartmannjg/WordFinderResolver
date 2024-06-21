using System.Net;
using System.Security;
using WordFinderResolver.Dto;
using WordFinderResolver.Service.Validations.Chains;

namespace WordFinderResolver.Service
{
    public class WordFinderService
    {
        private readonly WordFinderFactory _wordFinderFactory;
        private readonly MatrixValidationsChains _matrixValidationsChains;

        public WordFinderService(WordFinderFactory wordFinderFactory, MatrixValidationsChains matrixValidationsChains) 
        { 
            _wordFinderFactory = wordFinderFactory;
            _matrixValidationsChains = matrixValidationsChains;
        }

        /// <summary>
        /// Receives a MatrixColecctionDto and returns the first 10 most occurring words in the matrix.
        /// </summary>
        /// <param name="MatrixColecctionDto">dto with matrix and array with searching words.</param>
        /// <returns>List of the 10 most frequent words in the matrix ordered from highest to lowest</returns>
        public async Task<IEnumerable<string>> Resolve(MatrixColecctionDto dto) 
        {
            try
            {
                _matrixValidationsChains.GetValidations().Validate(dto);

                var matrixList = await ConvertMatrixToEnumerable(dto.Matrix);

                var wordFinder = _wordFinderFactory.CreateWordFinder(matrixList);

                return wordFinder.Find(dto.Words);
            }
            catch (Exception) 
            {
                throw;
            }

        }

        /// <summary>
        /// Receives an array and returns a list of strings consisting of each row and column.
        /// </summary>
        /// <param name="matrix">matrix of string to analize.</param>
        /// <returns>IEnumerable list of string from matrix</returns>
        private Task<IEnumerable<string>> ConvertMatrixToEnumerable(string[][] matrix)
        {
            List<string> result = new List<string>();

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.Length > 0 ? matrix[0].GetLength(0) : 0;

            //Add words from rows
            for (int i = 0; i < rowCount; i++)
            {
                string wordResult = "";
                for (int j = 0; j < colCount; j++)
                {
                    wordResult += matrix[i][j];
                }
                result.Add(wordResult);
            }

            //Add words from columns
            for (int j = 0; j < colCount; j++)
            {
                string wordResult = "";
                for (int i = 0; i < rowCount; i++)
                {
                    wordResult += matrix[i][j];
                }
                result.Add(wordResult);
            }


            IEnumerable<string> enumerableResult = result;

            return Task.FromResult<IEnumerable<string>>(enumerableResult);
        }
    }
}
