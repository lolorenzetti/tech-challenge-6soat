namespace Domain.Validators
{
    public interface IValidador<T>
    {
        public void Validar(T entity);
    }
}
