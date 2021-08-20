using FluentValidation;
using FluentValidation.Results;
using System;

namespace Ingenius.Domain
{
    public class Product:Entity<Product>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Size { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }
        public Actions Action { get; set; }

        public override bool IsValid()
        {
            Valid();
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        private void Valid()
        {
            RuleFor(p => p.Code).MinimumLength(3).WithMessage("Ingrese el codigo con mas de 3 letras");
            RuleFor(pessoa => pessoa.Code).NotEmpty().WithMessage("Ingrese el codigo");

            RuleFor(pessoa => pessoa.Name).NotEmpty().WithMessage("Ingrese el nombre");
            RuleFor(pessoa => pessoa.Name).MinimumLength(3).WithMessage("Ingrese el nombre con mas de 3 letras");

            RuleFor(pessoa => pessoa.Amount).GreaterThan(0).WithMessage("Ingrese el cantidad");

            RuleFor(pessoa => pessoa.Size).NotEmpty().WithMessage("Ingrese el Tamaño");
            
            RuleFor(pessoa => pessoa.Date).LessThanOrEqualTo(DateTime.Now).WithMessage("No es posible una fecha adelantada ");
        }
    }

    public enum Actions
    {
        Add = 1,
        Remove =2
    }
}
