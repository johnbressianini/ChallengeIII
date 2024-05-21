using ChallengeApi.Domain.DTOs;

namespace ChallengeApi.Domain.Models
{
    public interface INoticiaRepository
    {
        void Add(Noticia noticia);
        void Update(Noticia noticia);
        List<NoticiaDTO> GetAll(int pageNumber, int pageQuantity);
        NoticiaDTO? Get(int id);
    }
}
