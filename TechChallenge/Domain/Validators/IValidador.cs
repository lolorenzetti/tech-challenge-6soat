using Domain.Entities;

namespace Domain.Validators
{
    public interface IValidador<T> where T : Entity
    {
        public void Validar(T entity);
    }
}
