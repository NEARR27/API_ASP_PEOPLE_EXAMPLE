using PeopleApi.Controllers;

namespace PeopleApi.Services
{
    public interface IPeopleService
    {
        bool Validate(People people);
    }
}
