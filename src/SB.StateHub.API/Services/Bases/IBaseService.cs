namespace SB.StateHub.API.Services.Bases
{
    public interface IBaseService<T>
    {
        Task<D> GetByIdAsync<D>(int id);
        IEnumerable<D> GetAll<D>();
        Task<D> CreateOrUpdateAsync<D>(D dto);
        Task DeleteAsync(int id);
    }
}