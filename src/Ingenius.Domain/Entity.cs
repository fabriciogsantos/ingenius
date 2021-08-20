using FluentValidation;
using FluentValidation.Results;
using System;

namespace Ingenius.Domain
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        public Entity()
        {
            ValidationResult = new ValidationResult();
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public abstract bool IsValid();
        public ValidationResult ValidationResult { get; protected set; }
    }
}
