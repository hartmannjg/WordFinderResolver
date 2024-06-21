using WordFinderResolver.Service.Validations.Rules;

namespace WordFinderResolver.Service.Validations.Chains
{
    public class MatrixValidationsChains
    {
        private readonly MatrixLengthValidation _matrixLengthValidation;
        private readonly MatrixSquareValidation _matrixSquareValidation;

        public MatrixValidationsChains(MatrixLengthValidation matrixLengthValidation, MatrixSquareValidation matrixSquareValidation)
        {
            _matrixLengthValidation = matrixLengthValidation;
            _matrixSquareValidation = matrixSquareValidation;
        }

        public AbstractValidation GetValidations()
        {
            var chain = _matrixSquareValidation;
            chain.SetNext(_matrixLengthValidation);
            return chain;
        }
    }
}
