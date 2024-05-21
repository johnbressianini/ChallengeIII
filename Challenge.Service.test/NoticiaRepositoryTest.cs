using ChallengeApi.Domain.Models;
using ChallengeApi.Infraestrutura;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Challenge.Service.test
{
    public class NoticiaRepositoryTest
    {
        private readonly INoticiaRepository _repository;
        private readonly string title = string.Empty;
        private readonly string description = string.Empty;
        private readonly string author = string.Empty;
        public NoticiaRepositoryTest()
        {

            var service = new ServiceCollection();
            service.AddTransient<INoticiaRepository, NoticiaRepository>();
            var provedor = service.BuildServiceProvider();
            _repository = provedor.GetService<INoticiaRepository>();

            title = "Primeiro Titulo";
            description = "Conteudo xxxxxxx";
            author = "Autor numero 1";
        }

        [Fact]
        public void Obter_todas_noticias()
        {
            var noticias = _repository.GetAll(1, 100);
            Assert.NotNull(noticias);
        }

        [Fact]
        public void Obter_noticia()
        {
            var noticia = _repository.Get(1);
            Assert.NotNull(noticia);
        }

        [Fact]
        public void Add_noticia()
        {
            var noticias = new Noticia(title, author, description);
            _repository.Add(noticias);
           
            Assert.NotNull(noticias);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Obter_noticias_por_variosids(int id)
        {
            var noticias = _repository.Get(id);
            Assert.NotNull(noticias);
        }

        [Fact]
        public void Update_noticia()
        {
            string title = "update title";
            string Author = "Author title";
            string Description = "Description title";

            var noticias = _repository.Get(1);
            
            noticias.Title = title;
            noticias.Author = author;
            noticias.Description = description;

            var noticia = new Noticia(noticias.Title, noticias.Author, noticias.Description);

            _repository.Update(noticia);

            Assert.NotNull(noticias);
        }
    }
}
