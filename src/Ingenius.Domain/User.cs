using FluentValidation;

namespace Ingenius.Domain
{
    public class User : Entity<User>
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string CheckPassword { get; set; }

        public override bool IsValid()
        {
            Valid();
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        private void Valid()
        {
            RuleFor(p => p.Login).MinimumLength(3).WithMessage("Ingrese el Login con mas de 3 letras");
            RuleFor(p => p.Login).NotEmpty().WithMessage("Ingrese el Login");

            RuleFor(p => p.Name).NotEmpty().WithMessage("Ingrese el nombre");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Ingrese el nombre con mas de 3 letras");

            RuleFor(p => p.Password).MinimumLength(3).WithMessage("Ingrese la clave");
            RuleFor(p => p.Password).Equal(p => p.CheckPassword).WithMessage("Las clave son distintas");

        }
    }

    public static class UserActual
    {
        public static User userActual;

    }
}
