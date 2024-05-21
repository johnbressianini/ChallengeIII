using ChallengeApi.Infraestrutura;
using System;
using Xunit;

namespace Challenge.Service.test
{
    public class ContextTest
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        [Fact]
        public void Teste_conexao_bd()
        {   
            bool conectado;

            try
            {
                conectado = _context.Database.CanConnect();

            }
            catch (Exception e)
            {

                throw new Exception("Não foi possível conectar a base de dados.");
            }

            Assert.True(conectado);

        }
    }
}