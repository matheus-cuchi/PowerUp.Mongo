using MongoDB.Driver;
using PowerUp.Mongo.Abstracao;
using PowerUp.Mongo.Contextos;
using System.Linq.Expressions;

namespace PowerUp.Mongo.Implementacoes;

public class RepositorioBaseMongo<TEntidade> : IRepositorio<TEntidade>
    where TEntidade : EntidadeBase
{
    private readonly ContextoMongo _contextoMongo;

    public RepositorioBaseMongo(ContextoMongo contextoMongo)
    {
        _contextoMongo = contextoMongo;
    }

    public Task AdicionarAsync(TEntidade entidade)
    {
        return _contextoMongo
           .ObterCollection<TEntidade>()
           .InsertOneAsync(entidade);
    }

    public Task AtualizarAsync(TEntidade entidade)
    {
        return _contextoMongo
           .ObterCollection<TEntidade>()
           .ReplaceOneAsync(x => x.Id == entidade.Id, entidade);
    }

    public Task DeletarAsync(TEntidade entidade)
    {
        return _contextoMongo
           .ObterCollection<TEntidade>()
           .DeleteOneAsync(x => x.Id == entidade.Id);
    }

    public async Task<TEntidade> PrimeiroAsync(Expression<Func<TEntidade, bool>> filtro)
    {
        var cursor = await _contextoMongo
                        .ObterCollection<TEntidade>()
                        .FindAsync(filtro);

        return await cursor.FirstAsync();
    }

    public async Task<List<TEntidade>> FiltrarAsync(Expression<Func<TEntidade, bool>> filtro)
    {
        var cursor = await _contextoMongo
                        .ObterCollection<TEntidade>()
                        .FindAsync(filtro);

        return await cursor.ToListAsync();
    }
}