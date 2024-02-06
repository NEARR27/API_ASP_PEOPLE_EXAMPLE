using PeopleApi.DTOS;

namespace PeopleApi.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDTO>> Get();
    }
}
