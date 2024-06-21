using WordFinderResolver.Dto;

namespace WordFinderResolver.Service.Validations.Rules
{
    public class MatrixSquareValidation : AbstractValidation
    {
        public override void Validate(MatrixColecctionDto matrixColecctionDto)
        {
            int rowCount = matrixColecctionDto.Matrix.GetLength(0);
            int colCount = matrixColecctionDto.Matrix.Length > 0 ? matrixColecctionDto.Matrix[0].GetLength(0) : 0;

            if (rowCount != colCount )
            {
                throw new Exception("The matrix is not a square matrix");
            }

            base.Validate(matrixColecctionDto);
        }
    }
}
