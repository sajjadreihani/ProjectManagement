namespace ProjectManagement.Domain.SeedWork;

public interface IRepository<T, TKEY>
{
    T Add(T entity);
    void Update(T entity);
    Task<T> GetById(TKEY id, CancellationToken cancellationToken = default);
    void Delete(T entity);
    IUnitOfWork UnitOfWork { get; }
}
