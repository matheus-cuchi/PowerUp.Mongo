using MongoDB.Driver;
using PowerUp.Mongo.Abstracao;
using PowerUp.Mongo.Configracoes;

namespace PowerUp.Mongo.Contextos;

public class ContextoMongo
{
    private readonly IMongoDatabase _database;
    private static readonly IDictionary<string, string> _cacheNomeCollections;
    private static readonly Type _atributoAnutacao;

    static ContextoMongo()
    {
        _cacheNomeCollections = new Dictionary<string, string>();
        _atributoAnutacao = typeof(NomeColecaoMongoAttribute);
    }

    public ContextoMongo(IMongoDatabase database)
    {
        _database = database;
    }

    private static string ObterNomeDaColecao<TEntidade>()
    {
        var nomeTipo = typeof(TEntidade).Name;

        if (_cacheNomeCollections.ContainsKey(nomeTipo))
        {
            return _cacheNomeCollections[nomeTipo];
        }

        return ObterNomeDaColecaoCacheando<TEntidade>();
    }

    private static string ObterNomeDaColecaoCacheando<TEntidade>()
    {
        var tipo = typeof(TEntidade);
        var nomeTipo = tipo.Name;

        lock (_cacheNomeCollections)
        {
            if (_cacheNomeCollections.ContainsKey(nomeTipo))
            {
                return _cacheNomeCollections[nomeTipo];
            }

            var atributo = tipo
               .GetCustomAttributes(_atributoAnutacao, true)
               .FirstOrDefault();

            if (atributo is NomeColecaoMongoAttribute nomeColecaoMongoAtributo)
            {
                _cacheNomeCollections.Add(nomeTipo, nomeColecaoMongoAtributo.NomeColecao);

                return nomeColecaoMongoAtributo.NomeColecao;
            }

            _cacheNomeCollections.Add(nomeTipo, nomeTipo);

            return nomeTipo;
        }
    }

    public IMongoCollection<TEntidade> ObterCollection<TEntidade>()
        where TEntidade : EntidadeBase
    {
        return _database.GetCollection<TEntidade>(ObterNomeDaColecao<TEntidade>());
    }
}