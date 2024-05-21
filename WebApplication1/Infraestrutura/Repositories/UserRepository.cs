using ChallengeApi.Domain.DTOs;
using ChallengeApi.Models;

namespace ChallengeApi.Infraestrutura.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public void Add(User user)
        {
            _context.user.Add(user);
            _context.SaveChanges();
        }


        public UserDTO? Get(int id)
        {
            var user = _context.user.Find(id);
            return new UserDTO()
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
            };
        }

        public List<UserDTO> GetAll(int pageNumber, int pageQuantity)
        {
            return _context.user.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(x => new UserDTO()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Email = x.Email,
                })
                .ToList();
        }
    }
}
