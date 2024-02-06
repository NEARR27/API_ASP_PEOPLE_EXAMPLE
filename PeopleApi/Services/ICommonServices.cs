using PeopleApi.DTOS;

namespace PeopleApi.Services
{
    public interface ICommonServices<T, TI, TU>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);

        Task<T> Add(TI beerIdDTO);

        Task<T> Update(int id, TU beerUpdateDTO);

        Task<T> Delete(int id);

        bool Validate(TI dto);

        bool Validate(TU dto);
    }
}
