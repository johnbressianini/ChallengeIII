using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeApi.Domain.Models
{
    [Table("Noticia")]
    public class Noticia
    {
        [Key]
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }

        public Noticia(string title, string author, string description)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
