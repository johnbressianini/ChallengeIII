using Microsoft.AspNetCore.Mvc;
using ChallengeApi.ViewModel;
using ChallengeApi.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ChallengeApi.Controllers
{
    [ApiController]
    [Route("api/v1/noticia")]
    public class NoticiaController : ControllerBase
    {
        private readonly INoticiaRepository _noticiaRepository;
        private readonly ILogger<NoticiaController> _logger;
        private readonly IMapper _mapper;

        public NoticiaController(INoticiaRepository noticiaRepository, ILogger<NoticiaController> logger, IMapper mapper)
        {
            _noticiaRepository = noticiaRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
      

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromForm]NoticiaViewModel model)
        {
            _logger.Log(LogLevel.Information, "Add Noticias"); 

            var noticia = new Noticia(model.Title, model.Author, model.Description);
            _noticiaRepository.Add(noticia);
            return Ok();
        }

        [HttpGet(Name = "Noticias")]
        [Authorize]
        public IActionResult GetAll(int pageNumber, int pageQuantity)
        {
            _logger.Log(LogLevel.Information, "Get ALL Noticias");

            var noticias = _noticiaRepository.GetAll(pageNumber, pageQuantity);

            _logger.LogInformation("Fim Get ALL Noticias");

            return Ok(noticias);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Search(int id)
        {
            _logger.Log(LogLevel.Information, "Search Noticias");
            var noticia = _noticiaRepository.Get(id);

            var noticiaDTO = _mapper.Map<Noticia>(noticia);

            return Ok(noticiaDTO);
        }
    }
}
