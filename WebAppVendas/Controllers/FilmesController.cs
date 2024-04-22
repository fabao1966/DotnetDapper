using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppVendas.Repository;

namespace WebAppVendas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilmesController : ControllerBase
	{
		private readonly IFilmeRepository _repository;
		public FilmesController(IFilmeRepository repository) 
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task <IActionResult> Get()
		{
			var filmes = await _repository.BuscarFilmesAsync();

			return filmes.Any() ? Ok(filmes) : NoContent();
		}

	}
}
