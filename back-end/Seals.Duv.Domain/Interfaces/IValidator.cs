namespace Seals.Duv.Domain.Interfaces
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
