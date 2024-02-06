using FluentValidation;
using PeopleApi.DTOS;

namespace PeopleApi.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerIdDTO>
    {
        public BeerInsertValidator() { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(3, 25).WithMessage("El nombre debe tener entre 3 y 25 caracteres");
            RuleFor(x => x.BrandId).NotNull().WithMessage(x => "La marca es obligatoria");
            RuleFor(x => x.BrandId).GreaterThan(0).WithMessage(x => "Error con el enviado de marca");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage(x => "El {PropertyName} debe ser mayor a 0");
        }
        
    }
}
