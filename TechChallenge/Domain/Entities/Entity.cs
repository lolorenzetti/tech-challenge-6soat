using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; private set; }

        public List<ItemErro> Errors = new List<ItemErro>();

        public void AddError(ItemErro message)
        {
            Errors.Add(message);
        }

        public IEnumerable<ItemErro> GetErrors()
        {
            return Errors;
        }

        public bool HasErrors()
        {
            return Errors.Count > 0;
        }

        public bool Valid { get; private set; }
        public bool Invalid => !Valid;
        public ValidationResult ValidationResult { get; private set; } = new ValidationResult();

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }

        public abstract void Validar();
    }
}
