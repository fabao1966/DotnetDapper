using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppVendas.Models;
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

			return filmes.Any() 
				? Ok(filmes) 
				: NoContent();
		}

		[HttpGet("id")]
		public async Task<IActionResult> Get(int id)
		{
			var filme = await _repository.BuscaFilmeAsync(id);

			return filme != null 
						? Ok(filme) 
						: NotFound("Filme não encontrado");
		}

		[HttpPost]
		public async Task<IActionResult> Post(FilmeRequest request)
		{
            if (string.IsNullOrEmpty(request.Nome ) || request.Ano <= 0 || request.ProdutoraId <= 0)
            {
				return BadRequest("informações inválidas");
            }

			var adicionar = await _repository.AdicionaAsync(request);

			return adicionar 
				? Ok("Filma adicionado com sucesso")
				: BadRequest("Erro ao adicionar Filme");
        }

		[HttpPut("id")]
		public async Task<IActionResult> Put(FilmeRequest request, int id)
		{
			if (id <= 0) return BadRequest("Filme inválido");

			var filme = await _repository.BuscaFilmeAsync(id);

			if (filme == null) return NotFound("Filme não existe");

			if (string.IsNullOrEmpty(request.Nome)) request.Nome = filme.Nome;
			if (request.Ano <= 0) request.ProdutoraId = filme.Ano;

			var atualizar = await _repository.AtualizaAsync(request, id);

			return atualizar
				? Ok("Filme atualizado com sucesso")
				: BadRequest("Erro ao atualizar Filme");			
		}

		[HttpDelete("id")]
		public async Task<IActionResult> Delete( int id)
		{
			if (id <= 0) return BadRequest("Filme inválido");

			var filme = await _repository.BuscaFilmeAsync(id);

			if (filme == null) return NotFound("Filme não existe");

			var deletado = await _repository.DeletaAsync(id);

			return deletado
				? Ok("Filme deletado com sucesso")
				: BadRequest("Erro ao deletar Filme");
		}

	}
}
