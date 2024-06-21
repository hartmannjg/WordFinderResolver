using WordFinderResolver.Dto;

namespace WordFinderResolver.Service.Validations
{
    public class AbstractValidation
    {
        private AbstractValidation nextValidation;
        
        public AbstractValidation SetNext(AbstractValidation next)
        {
            nextValidation = next;
            return next;
        }

        public virtual void Validate (MatrixColecctionDto matrixColecctionDto)
        {
            nextValidation?.Validate(matrixColecctionDto);
        }
    }
}
