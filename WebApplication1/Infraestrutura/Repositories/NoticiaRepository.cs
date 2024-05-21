using ChallengeApi.Domain.DTOs;
using ChallengeApi.Domain.Models;

namespace ChallengeApi.Infraestrutura
{
    public class NoticiaRepository : INoticiaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public void Add(Noticia noticia)
        {
            _context.noticia.Add(noticia);
            _context.SaveChanges();
        }

        public NoticiaDTO? Get(int id)
        {
            var noticia = _context.noticia.Find(id);
            return new NoticiaDTO()
            {
                Id = noticia.Id,
                Author = noticia.Author,
                Title = noticia.Title,
                Description = noticia.Description,
            };
        }

        public List<NoticiaDTO> GetAll(int pageNumber, int pageQuantity)
        {
            return _context.noticia.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(x => new NoticiaDTO()
                {
                    Id = x.Id,
                    Author = x.Author,
                    Title = x.Title,
                    Description = x.Description,
                })
                .ToList();
        }

        public void Update(Noticia noticia)
        {
            _context.noticia.Update(noticia);
            _context.SaveChanges();
        }
    }
}
