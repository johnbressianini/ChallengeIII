using AutoMapper;
using ChallengeApi.Domain.DTOs;
using ChallengeApi.Domain.Models;

namespace ChallengeApi.Application.Maping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Noticia, NoticiaDTO>();
        }
    }
}
