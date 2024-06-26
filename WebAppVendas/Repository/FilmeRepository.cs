﻿using Dapper;
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
							Join tb_produtora p ON p.id = f.id_produtora;";

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

		public async Task<bool> AdicionaAsync(FilmeRequest request)
		{
			string sql = @"INSERT INTO 
							tb_filme(nome, ano, id_produtora)
							VALUES	
							(@Nome, @Ano, @ProdutoraId)";

			using var conn = new SqlConnection(connectionString);

			return await conn.ExecuteAsync(sql, request) > 0;
		}

		public async Task<bool> AtualizaAsync(FilmeRequest request, int id)
		{
			string sql = @"UPDATE tb_filme SET 
							nome = @Nome,
							ano = @Ano
							WHERE id = @Id";

			var parametros = new DynamicParameters();
			parametros.Add("Nome", request.Nome);
			parametros.Add("Ano", request.Ano);
			parametros.Add("Id", id);



			using var conn = new SqlConnection(connectionString);

			return await conn.ExecuteAsync(sql, parametros) > 0;
		}		

		public async Task<bool> DeletaAsync(int id)
		{
			string sql = @"DELETE FROM tb_filme 
							WHERE id = @Id";

			using var conn = new SqlConnection(connectionString);

			return await conn.ExecuteAsync(sql, new {Id = id}) > 0;

		}
	}
}
