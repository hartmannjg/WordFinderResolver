using WordFinderResolver.Service.Validations.Rules;

namespace WordFinderResolver.Service.Validations.Chains
{
    public class MatrixValidationsChains
    {
        private readonly MatrixLengthValidation _matrixLengthValidation;

        public MatrixValidationsChains(MatrixLengthValidation matrixLengthValidation)
        {
            _matrixLengthValidation = matrixLengthValidation;
        }

        public AbstractValidation GetValidations()
        {
            var chain = _matrixLengthValidation;
            return chain;
        }
    }
}
