using WordFinderResolver.Dto;

namespace WordFinderResolver.Service.Validations.Rules
{
    public class MatrixLengthValidation : AbstractValidation
    {
        public override void Validate(MatrixColecctionDto matrixColecctionDto)
        {
            int rowCount = matrixColecctionDto.Matrix.GetLength(0);
            int colCount = matrixColecctionDto.Matrix.Length > 0 ? matrixColecctionDto.Matrix[0].GetLength(0) : 0;

            if (rowCount > 64 || colCount > 64)
            {
                throw new Exception("Wrong size of the matrix");
            }

            base.Validate(matrixColecctionDto);
        }
    }
}
