using Time_Sheet_WebApp.Controllers.Requests;
using Time_Sheet_WebApp.Database;
using Time_Sheet_WebApp.Entities;

namespace Time_Sheet_WebApp.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public User? GetById(Guid userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Add(User user)
        {
            user.IsDeleted = false;
            user.Id = Guid.NewGuid(); 
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(Guid userId, UpdateUserRequestDTO request)
        {
            var userFound = GetById(userId) ??
                throw new ArgumentException($"Cannot find user with the following Id: '{userId}'");

            userFound.Name = request.Name;
            userFound.Email = request.Email;
            userFound.Password = request.Password;
            userFound.IsDeleted = request.IsDeleted;
            _context.SaveChanges();
        }

        // Soft Delete
        public void Delete(Guid userId)
        {
            var user = GetById(userId) ??
                throw new ArgumentException($"Cannot find user with the following Id: '{userId}'");

            user.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
