using WebAppVendas.Models;

namespace WebAppVendas.Repository
{
	public interface IFilmeRepository
	{
		Task<IEnumerable<FilmeResponse>> BuscarFilmesAsync();
		Task<FilmeResponse> BuscaFilmeAsync(int id);
		Task<bool> AdicionaAsync(FilmeRequest request);
		Task<bool> AtualizaAsync(FilmeRequest request, int id);
		Task<bool> DeletaAsync(int id);
	}
}
