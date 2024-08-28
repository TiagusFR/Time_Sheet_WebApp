using Time_Sheet_WebApp.Controllers.Requests;
using Time_Sheet_WebApp.Entities;

namespace Time_Sheet_WebApp.Services
{
    public interface IUserService
    {
        User? GetById(Guid id);
        List<User> GetAll();
        User Add(User user);
        void Update(Guid id, UpdateUserRequestDTO request);
        void Delete(Guid id);
    }
}
