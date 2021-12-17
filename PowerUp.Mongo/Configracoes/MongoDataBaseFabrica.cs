using MongoDB.Driver;
using PowerUp.Mongo.Abstracao;

namespace PowerUp.Mongo.Configracoes;

public class MongoDataBaseFabrica : IMongoDataBaseFabrica
{
    private readonly ConfiguracaoMongo _configuracaoMongo;

    public MongoDataBaseFabrica(ConfiguracaoMongo configuracaoMongo)
    {
        _configuracaoMongo = configuracaoMongo;
    }

    public IMongoDatabase CriarMongoDataBase()
    {
        return new MongoClient(_configuracaoMongo.ConnectionString)
           .GetDatabase(_configuracaoMongo.NomeDataBase);
    }
}