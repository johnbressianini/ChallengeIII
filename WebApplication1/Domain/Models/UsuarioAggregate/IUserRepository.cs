using ChallengeApi.Domain.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeApi.Models
{
    public interface IUserRepository
    {
        void Add(User user);
        List<UserDTO> GetAll(int pageNumber, int pageQuantity);
        UserDTO? Get(int id);
    }
}
