using FusionStackBackEnd.Controllers;
using FusionStackBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace FusionStackBackEnd.Repository
{
    public class LoginRepositoryImpl : LoginRepository
    {
        private readonly AppDbContext _context;

        public LoginRepositoryImpl(AppDbContext context)
        {
            _context = context;
        }
        public User getUser(string email, string password,string userRole)
        {
            Console.WriteLine(password);
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.Email == email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password)) return null;
                Role role = _context.Roles.FirstOrDefault(u => u.Id == user.RoleId);
                user.Role = role;
                if (role.Name != userRole)
                    return null;
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while retrieving user credentials: {ex.Message}");
                return null;
            }

        }

        internal int pageCount()
        {
            return _context.Users.Count();


        }

        internal void UpdateUser(RegisterModelDto  user)
        {
            var users = _context.Users.FirstOrDefault(p => p.Id == user.Id);

            if (users != null)
            {

                users.Email = user.Email;
                users.Phone = user.Phone;
                users.Name = user.Name;


                _context.Entry(users).State = EntityState.Modified;

                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        internal void deleteUser(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(p => p.Id == id);

            if (userToDelete != null)
            {

                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }

        public List<User> getUsers(int page, int pageSize)
        { 


            return _context.Users.Skip((page) * pageSize).Take(pageSize).ToList();


        }
        public Role getRole(string role)
        {
            return _context.Roles.FirstOrDefault(u => u.Name == role);
        }
    }
}