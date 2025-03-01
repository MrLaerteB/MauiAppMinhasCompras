using MauiAppMinhasCompras.Models; //Cria um "atalho", para não precisar escrever tudo, sempre
using SQLite; //Usando biblioteca SQLite

namespace MauiAppMinhasCompras.Helpers //Organiza o código em grupos lógicos e evita redundância
{
    public class SQLiteDatabaseHelper //Classe que gerencia o banco de dados
    {
        readonly SQLiteAsyncConnection _conn; //Conecta de forma assíncrona com o BD
        public SQLiteDatabaseHelper(string path) //Define o construtor da classe
        {
            _conn = new SQLiteAsyncConnection(path); //Inicializa a conexão com BD 
            _conn.CreateTableAsync<Produto>().Wait(); //Cria a tabela Produto, se ainda não existir
        }
        public Task<int> Insert(Produto p) //Insere um produto no BD
        {
            return _conn.InsertAsync(p);
        }
        public Task<List<Produto>> Update(Produto p) //Atualiza os dados de um produto no BD
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(
            sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }
        public Task<int> Delete(int id) //Exclui um produto do BD
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
        public Task<List<Produto>> GetAll() //Retorna todos os produtos cadastrados no BD
        {
            return _conn.Table<Produto>().ToListAsync();
        }
        public Task<List<Produto>> Search(string q) //Busca produtos no BD, de acordo com uma palavra-chave
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}