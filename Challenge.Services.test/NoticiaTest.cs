using Xunit;
using ChallengeApi.Domain.Models;
using System;
using FluentAssertions;

namespace Challenge.Services.test
{

    public class NoticiaTest
    {
        private string title = string.Empty;
        private string description = string.Empty;
        private string author = string.Empty;

        public NoticiaTest()
        {
            title = "Primeiro Titulo";
            description = "Conteudo 1";
            author = "Autor numero 1";
        }

        [Fact]
        public void Criar_noticia_com_sucesso()
        {
            var noticia = new Noticia(title, author, description);
            noticia.Title.Should().Be(title);
            noticia.Description.Should().Be(description);
            noticia.Author.Should().Be(author);
        }

        [Fact]
        public void Validar_erro_nao_informando_titulo()
        {
            Assert.Throws<ArgumentNullException>(() => new Noticia(null,  author, description));
        }

        [Fact]
        public void Validar_erro_nao_informando_conteudo()
        {
            Assert.Throws<ArgumentNullException>(() => new Noticia(title, author, null));
        }

        [Fact]
        public void Validar_erro_nao_informando_autor()
        {
            Assert.Throws<ArgumentNullException>(() => new Noticia(title, null, description));
        }
    }
}