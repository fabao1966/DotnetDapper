using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using WebAppVendas.Models;

namespace WebAppVendas.Repository
{
	public class FilmeRepository : IFilmeRepository
	{
		private readonly IConfiguration _configuration;
		private readonly string connectionString;

		public FilmeRepository(IConfiguration configuration) 
		{ 
			_configuration = configuration;
			connectionString = _configuration.GetConnectionString("sqlConnection");
		}
		public async Task<IEnumerable<FilmeResponse>> BuscarFilmesAsync()
		{
			string sql = @"select f.id Id,
							   f.nome Nome,
							   f.ano Ano,
							   p.nome Produtora 
							from tb_filme f
							Join tb_produtora p ON f.id = p.id;";

			using var conn = new SqlConnection(connectionString);
			
			return await conn.QueryAsync<FilmeResponse>(sql);
			
		}

		public async Task<FilmeResponse> BuscaFilmeAsync(int id)
		{
			string sql = @"select f.id Id,
							   f.nome Nome,
							   f.ano Ano,
							   p.nome Produtora 
							from tb_filme f
							Join tb_produtora p ON f.id = p.id
							where f.id = @Id;";
			using var conn = new SqlConnection(connectionString);

			return await conn.QueryFirstOrDefaultAsync<FilmeResponse>(sql, new { Id = id });
		}

		public Task<bool> AdicionaAsync(FilmeRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<bool> AtualizaAsync(FilmeRequest request, int id)
		{
			throw new NotImplementedException();
		}		

		public Task<bool> DeletaAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
