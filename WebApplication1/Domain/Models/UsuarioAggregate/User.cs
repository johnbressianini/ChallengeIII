using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeApi.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public User()
        {
            Nome = string.Empty;
            Email = string.Empty;
        }

        public User(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}
